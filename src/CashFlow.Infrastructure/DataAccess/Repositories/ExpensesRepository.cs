using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Infrastructure.DataAccess.Repositories;
internal class ExpensesRepository() : IExpensesRepository
{
    void IExpensesRepository.Add(Expense expense)
    {
        var dbContext = new CashFlowDbContext();
        dbContext.Expenses.Add(expense);
        dbContext.SaveChanges();
    }
}
