using System;

namespace CashFlow.Communication.Requests;

public class RequestLoginUser
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
