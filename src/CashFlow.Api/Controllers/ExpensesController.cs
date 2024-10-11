using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{

    [HttpPost]
    public IActionResult Register([FromBody] RequestRegisterExpense expense)
    {
        try
        {
            var useCase = new RegisterExpenseUseCase();
            var res = useCase.Execute(expense);
            return Created(string.Empty, res);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch
        {

            return StatusCode(StatusCodes.Status500InternalServerError, "unknown error");
        }
    }
}
