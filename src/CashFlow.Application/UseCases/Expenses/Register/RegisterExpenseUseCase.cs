using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase
{
    public ResponseRegisteredExpense Execute(RequestRegisterExpense expense)
    {
        Validate(expense);

        return new ResponseRegisteredExpense
        {
            Title = expense.Title
        };
    }

    private void Validate(RequestRegisterExpense expense)
    {
        var validator = new RegisterExpenseValidator();
        var result = validator.Validate(expense);

        if(!result.IsValid)
        {
            result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ArgumentException(result.Errors[0].ErrorMessage);
        }
    }
}
