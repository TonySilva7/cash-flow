﻿using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IExpensesReadOnlyRepository
{
    Task<List<Expense>> GetAllAsync();
    Task<Expense?> GetByIdAsync(Guid id);
    Task<List<Expense>> GetByMonthAsync(DateOnly month);
}
