﻿using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteredExpense), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseError), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromServices] IRegisterExpenseUseCase useCase, [FromBody] RequestRegisterExpense expense)
    {
        var res = await useCase.Execute(expense);
        return Created(string.Empty, res);
    }
}
