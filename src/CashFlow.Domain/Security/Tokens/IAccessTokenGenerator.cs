namespace CashFlow.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    public string Generate(Guid userIdentifier);
    public string GenerateRefreshToken();
}
