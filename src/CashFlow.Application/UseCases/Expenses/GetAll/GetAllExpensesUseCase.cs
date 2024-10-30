using System;
using AutoMapper;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses;

public class GetAllExpensesUseCase(IExpensesReadOnlyRepository expensesRepository, IMapper mapper) : IGetAllExpensesUseCase
{
    public async Task<ResponseExpenses> Execute()
    {
        var result = await expensesRepository.GetAllAsync();

        return new ResponseExpenses
        {
            Expenses = mapper.Map<List<ResponseShortExpense>>(result)
        };
    }
}
