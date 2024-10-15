using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CashFlow.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExpensesController : ControllerBase
{

    [HttpPost]
    public IActionResult Register([FromServices] IRegisterExpenseUseCase useCase, [FromBody] RequestRegisterExpense expense)
    {
        var res = useCase.Execute(expense);
        return Created(string.Empty, res);
    }
}
