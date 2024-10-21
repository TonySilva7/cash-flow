using Bogus;
using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;

namespace CommonTestUtilities.Requests;
public class RequestRegisterExpenseBuilder
{
    public static RequestRegisterExpense Build()
    {
        var request = new Faker<RequestRegisterExpense>("pt_BR")
            .RuleFor(r => r.Title, (faker) => faker.Commerce.ProductName())
            .RuleFor(r => r.Amount, (faker) => Math.Round(faker.Random.Decimal(1, 1000), 2))
            .RuleFor(r => r.Date, (faker) => faker.Date.Past())
            .RuleFor(r => r.PaymentType, (faker) => faker.PickRandom<PaymentType>())
            .RuleFor(r => r.Description, (faker) => faker.Commerce.ProductDescription());

        return request;
    }
}
