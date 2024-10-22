using System;

namespace CashFlow.Communication.Responses;

public class ResponseRefreshToken
{
    public ResponseToken Token { get; set; } = default!;
    public Guid RefreshToken { get; set; }
}
