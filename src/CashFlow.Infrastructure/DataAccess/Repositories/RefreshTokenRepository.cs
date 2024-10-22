using System;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Tokens;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;

internal class RefreshTokenRepository(CashFlowDbContext dbContext) : IRefreshTokenRepository
{
    public async Task AddAsync(RefreshToken refreshToken)
    {
        await dbContext.RefreshTokens.AddAsync(refreshToken);
    }

    public Task<RefreshToken?> GetByRefreshTokenAsync(string refreshToken)
    {
        return dbContext.RefreshTokens.AsNoTracking().FirstOrDefaultAsync(token => token.Value == refreshToken);
    }

    public async Task<RefreshToken?> GetByUserIdAsync(Guid userId)
    {
        var result = await dbContext.RefreshTokens
            .Include(token => token.User)
            .Where(token => token.User.UserIdentifier == userId)
            .FirstOrDefaultAsync();

        return result;
    }

    public void RemoveAsync(RefreshToken refreshToken)
    {
        dbContext.RefreshTokens.Remove(refreshToken);
    }

    public async Task RemoveByUserIdAsync(Guid userId)
    {
        var token = await dbContext.RefreshTokens.FirstOrDefaultAsync(token => token.UserId == userId);
        if (token is not null)
        {
            dbContext.RefreshTokens.Remove(token);
        }
    }

    public async Task SaveRefreshTokenAsync(RefreshToken refreshToken)
    {
        var tokens = dbContext.RefreshTokens.Where(token => token.UserId == refreshToken.UserId);
        dbContext.RefreshTokens.RemoveRange(tokens);

        await AddAsync(refreshToken);
    }
}
