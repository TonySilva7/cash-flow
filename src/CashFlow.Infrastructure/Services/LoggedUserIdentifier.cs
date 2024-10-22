using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CashFlow.Domain.Security.Tokens;

namespace CashFlow.Infrastructure.Services;

public class LoggedUserIdentifier(ITokenProvider tokenProvider) : IUserIdentifierProvider
{
    public Guid GetUserIdentifier()
    {
        var token = tokenProvider.Value();
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtSeurityToken = tokenHandler.ReadJwtToken(token);
        var identifier = jwtSeurityToken.Claims.First(claim => claim.Type == ClaimTypes.Sid).Value;

        return Guid.Parse(identifier);
    }
}
