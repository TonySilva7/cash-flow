using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using CashFlow.Api.Attributes;
namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{

    [HttpPost]
    [ProducesResponseType<ResponseRegisteredExpense>(StatusCodes.Status201Created)]
    [ProducesResponseType<ResponseError>(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(
        Summary = "Register a new expense",
        Description = "Register a new expense in the system",
        OperationId = "RegisterExpense",
        Tags = ["Expenses"]
    )]
    public async Task<IActionResult> Register([FromServices] IRegisterExpenseUseCase useCase, [FromBody] RequestRegisterExpense expense)
    {
        var res = await useCase.Execute(expense);
        return Created(string.Empty, res);
    }

    [HttpGet]
    [ProducesResponseType<ResponseExpenses>(StatusCodes.Status200OK)]
    [ProducesResponseType<ResponseError>(StatusCodes.Status400BadRequest)]
    [SwaggerOperation(
        Summary = "Get all expenses",
        Description = "Get all expenses registered in the system",
        OperationId = "GetAllExpenses",
        Tags = ["Expenses"]
    )]
    [AuthenticatedUser(["Vendedor", "Admin"])]
    public async Task<IActionResult> GetAllExpenses([FromServices] IGetAllExpensesUseCase useCase)
    {
        var res = await useCase.Execute();

        if (res.Expenses.Count != 0)
            return Ok(res);
        return NoContent();
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType<ResponseExpense>(StatusCodes.Status200OK)]
    [ProducesResponseType<ResponseError>(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = "Get an expense by id",
        Description = "Get an expense by id registered in the system",
        OperationId = "GetExpenseById",
        Tags = ["Expenses"]
    )]
    public async Task<IActionResult> GetExpenseById([FromServices] IGetExpenseByIdUseCase useCase, [FromRoute] Guid id)
    {
        var res = await useCase.Execute(id);

        if (res != null)
            return Ok(res);
        return NotFound();
    }
}
