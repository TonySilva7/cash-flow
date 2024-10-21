using CashFlow.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace CashFlow.Domain.Entities;

//[Table("Tbl_Expenses")]
public class Expense
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public PaymentType PaymentType { get; set; }
}
