using System;

namespace CashFlow.Communication.Responses;

public class ResponseRegisteredUser
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public ResponseRefreshToken Auth { get; set; } = default!;
}
