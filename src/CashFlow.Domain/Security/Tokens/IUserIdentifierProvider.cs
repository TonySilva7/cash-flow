using System;

namespace CashFlow.Domain.Security.Tokens;

public interface IUserIdentifierProvider
{
    Guid GetUserIdentifier();
}
