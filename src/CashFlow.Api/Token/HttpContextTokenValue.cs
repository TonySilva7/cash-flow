using System;
using CashFlow.Domain.Security.Tokens;

namespace CashFlow.Api.Token;

public class HttpContextTokenValue(IHttpContextAccessor contextAccessor) : ITokenProvider
{
    public string Value()
    {
        var authentication = contextAccessor.HttpContext!.Request.Headers.Authorization.ToString();

        if (string.IsNullOrEmpty(authentication))
        {
            throw new UnauthorizedAccessException("Token not found");
        }

        return authentication.Replace("Bearer ", string.Empty);
    }
}
