using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Exception.ExceptionBase;

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

        if (!result.IsValid)
        {
            var errorsMessage = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorsMessage);
        }
    }
}
