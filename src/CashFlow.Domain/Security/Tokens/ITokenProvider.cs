using System;

namespace CashFlow.Domain.Security.Tokens;

public interface ITokenProvider
{
    public string Value();
}
