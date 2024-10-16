using CashFlow.Domain.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Exception.ExceptionBase;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase(IExpensesRepository expensesRepository, IUnitOfWork unitOfWork) : IRegisterExpenseUseCase
{
    public ResponseRegisteredExpense Execute(RequestRegisterExpense expense)
    {
        Validate(expense);

        var entity = new Expense
        {
            //Id = Guid.NewGuid(),
            Amount = expense.Amount,
            Date = expense.Date,
            Description = expense.Description,
            PaymentType = (PaymentType)expense.PaymentType,
            Title = expense.Title,
        };

        expensesRepository.Add(entity);
        unitOfWork.Commit();

        return new ResponseRegisteredExpense
        {
            Title = expense.Title
        };
    }

    private static void Validate(RequestRegisterExpense expense)
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
