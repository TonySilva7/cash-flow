namespace CashFlow.Domain.Security.Tokens;

public interface IAccessTokenValidator
{
    public (Guid identifier, List<string>? roles) ValidateAndGetUserIdentifier(string accessToken);
}
