using AutoMapper;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.Update;

public class UpdateExpenseUseCase(IMapper mapper, IUnitOfWork unitOfWork, IExpenseUpdateOnlyRepository repository) : IUpdateExpenseUseCase
{
    public async Task Execute(Guid id, RequestExpense expense)
    {
        Validate(expense);
        var expenseToUpdate = await repository.GetByIdAsync(id) ?? throw new NotFoundException("Despesa não encontrada");

        var expenseMapped = mapper.Map(expense, expenseToUpdate);

        repository.Update(expenseMapped);

        await unitOfWork.Commit();
    }

    private void Validate(RequestExpense expense)
    {
        var validator = new ExpenseValidator();
        var validationResult = validator.Validate(expense);

        if (!validationResult.IsValid)
        {
            var errorMessage = validationResult.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessage);
        }
    }
}
