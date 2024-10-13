using System;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionBase;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CashFlow.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
	public void OnException(ExceptionContext context)
	{
		if (context.Exception is CashFlowException)
		{
			HandleProjectException(context);
		}
		else
		{
			ThrowUnknownError(context);
		}
	}

	private void HandleProjectException(ExceptionContext context)
	{
		switch (context.Exception)
		{
			case ErrorOnValidationException ex:
				var errorResponse = new ResponseError(ex.Errors);
				context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
				context.Result = new BadRequestObjectResult(errorResponse);
				break;
			default:
				// ThrowUnknownError(context);
				break;
		}
	}

	private void ThrowUnknownError(ExceptionContext context)
	{
		var errorResponse = new ResponseError("Unknown error occurred");
		context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
		context.Result = new ObjectResult(errorResponse);
	}
}
