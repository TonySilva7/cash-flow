using System;
using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Tokens;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken refreshToken);
    Task<RefreshToken?> GetByRefreshTokenAsync(Guid refreshToken);
    Task<RefreshToken?> GetByUserIdAsync(Guid userId);
    void RemoveAsync(RefreshToken token);
    Task RemoveByUserIdAsync(Guid userId);
    Task SaveRefreshTokenAsync(RefreshToken refreshToken);
}
