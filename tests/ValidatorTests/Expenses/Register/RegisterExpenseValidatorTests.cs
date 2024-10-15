using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Exception;
using CommonTestUtilities.Requests;
using FluentAssertions;


namespace ValidatorTests.Expenses.Register;
public class RegisterExpenseValidatorTests
{

    [Fact]
    public void Success_WhenRegisterExpenseIsValid()
    {
        // Arrange
        var request = RequestRegisterExpenseBuilder.Build();
        var validator = new RegisterExpenseValidator();

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ShouldReturnError_WhenTitleIsEmpty()
    {
        // Arrange
        var request = RequestRegisterExpenseBuilder.Build();
        request.Title = string.Empty;
        var validator = new RegisterExpenseValidator();

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .ContainSingle()
            .And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));
    }

    [Fact]
    public void ShouldReturnError_WhenAmountIsZero()
    {
        // Arrange
        var request = RequestRegisterExpenseBuilder.Build();
        request.Amount = 0;
        var validator = new RegisterExpenseValidator();

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .ContainSingle()
            .And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUT_BE_GREATER_THAN_ZERO));
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(-3)]
    public void ShouldReturnError_WhenAmountIsLessThanZero(decimal amount)
    {
        // Arrange
        var request = RequestRegisterExpenseBuilder.Build();
        request.Amount = amount;
        var validator = new RegisterExpenseValidator();

        // Act
        var result = validator.Validate(request);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should()
            .ContainSingle()
            .And
            .Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUNT_MUT_BE_GREATER_THAN_ZERO));
    }
}
