using System;
using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Expenses.GetById;

public class GetExpenseByIdUseCase(IExpensesRepository repository, IMapper mapper) : IGetExpenseByIdUseCase
{
    public async Task<ResponseExpense> Execute(Guid id)
    {
        var result = await repository.GetByIdAsync(id)
            ?? throw new NotFoundException(ResourceErrorMessages.EXPENSE_NOT_FOUND);

        return mapper.Map<ResponseExpense>(result);
    }
}
