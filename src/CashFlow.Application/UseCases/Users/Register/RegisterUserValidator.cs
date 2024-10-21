using CashFlow.Communication.Requests;
using CashFlow.Exception;
using FluentValidation;

namespace CashFlow.Application.UseCases.Users.Register;
public class RegisterUserValidator : AbstractValidator<RequestRegisterUser>
{
    public RegisterUserValidator()
    {
        RuleFor(registerExpense => registerExpense.Name)
            .NotEmpty()
            .WithMessage("Is required");

        RuleFor(registerExpense => registerExpense.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("Is required or invalid");

        RuleFor(registerExpense => registerExpense.Password)
            .NotEmpty()
            .MinimumLength(6)
            .WithMessage("Is required");

        RuleFor(registerExpense => registerExpense.ConfirmPassword)
            .Equal(registerExpense => registerExpense.Password)
            .WithMessage("Passwords do not match");
    }
}
