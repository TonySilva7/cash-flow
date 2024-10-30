using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class ExpenseValidator : AbstractValidator<RequestExpense>
{
    public ExpenseValidator()
    {
        RuleFor(registerExpense => registerExpense.Title)
            .NotEmpty()
            .WithMessage(ResourceErrorMessages.TITLE_REQUIRED);

        RuleFor(registerExpense => registerExpense.Amount)
            .GreaterThan(0)
            .WithMessage(ResourceErrorMessages.AMOUNT_MUT_BE_GREATER_THAN_ZERO);

        RuleFor(registerExpense => registerExpense.Date)
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage(ResourceErrorMessages.EXPENSES_CANNOT_FOR_THE_FUTURE);

        RuleFor(registerExpense => registerExpense.PaymentType)
            .IsInEnum()
            .WithMessage(ResourceErrorMessages.PAEMNT_TYPE_INVALID);
    }
}
