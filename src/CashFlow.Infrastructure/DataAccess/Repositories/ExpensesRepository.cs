using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;
internal class ExpensesRepository(CashFlowDbContext dbContext) : IExpensesReadOnlyRepository, IExpensesWriteOnlyRepository, IExpenseUpdateOnlyRepository
{
    public async Task AddAsync(Expense expense)
    {
        await dbContext.Expenses.AddAsync(expense);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var expense = await dbContext.Expenses.FirstOrDefaultAsync(expense => expense.Id == id);
        if (expense is not null)
        {
            dbContext.Expenses.Remove(expense);
            return true;
        }
        return false;
    }

    public async Task<List<Expense>> GetAllAsync()
    {
        return await dbContext.Expenses.AsNoTracking().ToListAsync();
    }

    async Task<Expense?> IExpensesReadOnlyRepository.GetByIdAsync(Guid id)
    {
        return await dbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(expense => expense.Id == id);
    }

    async Task<Expense?> IExpenseUpdateOnlyRepository.GetByIdAsync(Guid id)
    {
        return await dbContext.Expenses.FirstOrDefaultAsync(expense => expense.Id == id);
    }

    public void Update(Expense expense)
    {
        dbContext.Expenses.Update(expense);
    }

    public async Task<List<Expense>> GetByMonthAsync(DateOnly month)
    {
        var daysInMonth = DateTime.DaysInMonth(month.Year, month.Month);

        var startDate = new DateTime(month.Year, month.Month, 1).Date;
        var endDate = startDate.AddDays(daysInMonth - 1).Date;
        // var endDate = startDate.AddMonths(1).AddDays(-1).Date;

        return await dbContext.Expenses.AsNoTracking()
            .Where(expense => expense.Date >= startDate && expense.Date <= endDate)
            .OrderBy(expense => expense.Date)
            .ThenBy(expense => expense.Title)
            .ToListAsync();
    }
}
