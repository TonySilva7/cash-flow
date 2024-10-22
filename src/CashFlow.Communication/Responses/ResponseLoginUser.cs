using System;

namespace CashFlow.Communication.Responses;

public class ResponseLoginUser
{
    public Guid UserIdentifier { get; set; }
    public ResponseRefreshToken Auth { get; set; } = default!;
}
