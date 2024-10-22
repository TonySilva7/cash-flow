using System;

namespace CashFlow.Communication.Responses;

public class ResponseLoggedUser
{
    public Guid UserIdentifier { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
