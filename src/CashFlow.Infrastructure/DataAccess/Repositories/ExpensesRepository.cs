using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Infrastructure.DataAccess.Repositories;
internal class ExpensesRepository(CashFlowDbContext dbContext) : IExpensesRepository
{
    void IExpensesRepository.Add(Expense expense)
    {
        dbContext.Expenses.Add(expense);
    }
}
