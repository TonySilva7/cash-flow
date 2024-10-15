using CashFlow.Domain.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Infrastructure.DataAccess;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase
{
    public ResponseRegisteredExpense Execute(RequestRegisterExpense expense)
    {
        Validate(expense);

        //var dbContext = new CashFlowDbContext();
        //var entity = new Expense
        //{
        //    //Id = Guid.NewGuid(),
        //    Amount = expense.Amount,
        //    Date = expense.Date,
        //    Description = expense.Description,
        //    PaymentType = (PaymentType)expense.PaymentType,
        //    Title = expense.Title,

        //};


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
