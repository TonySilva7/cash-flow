using System;

namespace CashFlow.Communication.Responses;

public class ResponseLoginUser
{
    public Guid UserIdentifier { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ResponseToken Token { get; set; } = default!;
}
