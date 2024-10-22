using System;

namespace CashFlow.Communication.Responses;

public class ResponseRefreshToken
{
    public ResponseToken Token { get; set; } = default!;
    public string RefreshToken { get; set; } = string.Empty;
}
