using System;

namespace CashFlow.Communication.Requests;

public class RequestRefreshTokenUser
{
    public string Token { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}
