using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register;
internal class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpense>
{
    public RegisterExpenseValidator()
    {
        RuleFor(registerExpense => registerExpense.Title)
            .NotEmpty()
            .WithMessage("Title is required");

        RuleFor(registerExpense => registerExpense.Amount)
            .GreaterThan(0)
            .WithMessage("Amount must be greater than zero");

        RuleFor(registerExpense => registerExpense.Date)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Date is required");

        RuleFor(registerExpense => registerExpense.PaymentType)
            .IsInEnum()
            .WithMessage("Payment type is invalid");
    }
}
