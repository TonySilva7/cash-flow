using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;

public interface IExpensesWriteOnlyRepository
{
    Task AddAsync(Expense expense);

    /// <summary>
    /// Delete an expense by id and return TRUE if the expense was deleted or FALSE if the expense was not found.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Return an Task of bool</returns>
    Task<bool> DeleteAsync(Guid id);
}
