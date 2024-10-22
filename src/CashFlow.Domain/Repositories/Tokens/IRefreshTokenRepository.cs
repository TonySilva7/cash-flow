using System;
using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Tokens;

public interface IRefreshTokenRepository
{
    Task AddAsync(RefreshToken refreshToken);
    Task<RefreshToken?> GetByRefreshTokenAsync(string refreshToken);
    Task<RefreshToken?> GetByUserIdAsync(Guid userId);
    void RemoveAsync(RefreshToken refreshToken);
    Task RemoveByUserIdAsync(Guid userId);
    Task SaveRefreshTokenAsync(RefreshToken refreshToken);
}
