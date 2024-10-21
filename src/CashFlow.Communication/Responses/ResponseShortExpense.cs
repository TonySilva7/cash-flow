using System;

namespace CashFlow.Communication.Responses;

public class ResponseShortExpense
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public decimal Ammount { get; set; }
}
