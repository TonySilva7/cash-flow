using System;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register;

public interface IGetAllExpensesUseCase
{
    Task<ResponseExpenses> Execute();
}
