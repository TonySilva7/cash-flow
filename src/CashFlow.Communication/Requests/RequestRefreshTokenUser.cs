using System;

namespace CashFlow.Communication.Requests;

public class RequestRefreshTokenUser
{
    public string Token { get; set; } = string.Empty;
    public Guid RefreshToken { get; set; }
}
