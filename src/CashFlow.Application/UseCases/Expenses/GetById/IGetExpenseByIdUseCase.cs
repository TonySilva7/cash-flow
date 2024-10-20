using System;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.GetById;

public interface IGetExpenseByIdUseCase
{
  Task<ResponseExpense> Execute(Guid id);
}
