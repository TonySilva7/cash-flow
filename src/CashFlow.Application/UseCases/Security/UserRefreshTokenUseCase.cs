using System;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Tokens;
using CashFlow.Domain.Security.Tokens;

namespace CashFlow.Application.UseCases.Security;

public class UserRefreshTokenUseCase(
    IRefreshTokenRepository refreshRepository,
    IUserIdentifierProvider identifierProvider,
    IAccessTokenGenerator accessTokenGenerator,
    IUnitOfWork unitOfWork) : IUserRefreshTokenUseCase
{
    public async Task<ResponseLoginUser> Execute(RequestRefreshTokenUser request)
    {
        var userIdentifier = identifierProvider.GetUserIdentifier();
        var refreshTokenFromDb = await refreshRepository.GetByUserIdAsync(userIdentifier) ?? throw new UnauthorizedAccessException("Invalid token");



        var refreshTokenValidUntil = refreshTokenFromDb!.CreatedOn.AddDays(7);
        var isExpired = DateTime.Compare(refreshTokenValidUntil, DateTime.UtcNow) < 0;

        if (isExpired)
        {
            throw new UnauthorizedAccessException("Token expired");
        }

        if (refreshTokenFromDb is null || refreshTokenFromDb.Value != request.RefreshToken)
        {
            throw new UnauthorizedAccessException("Invalid token");
        }

        refreshTokenFromDb.Value = Guid.NewGuid();
        refreshTokenFromDb.UserId = userIdentifier;
        refreshTokenFromDb.CreatedOn = DateTime.Now.AddHours(1);
        var newToken = accessTokenGenerator.Generate(userIdentifier);

        await refreshRepository.SaveRefreshTokenAsync(refreshTokenFromDb);
        await unitOfWork.Commit();

        return new ResponseLoginUser
        {
            UserIdentifier = refreshTokenFromDb.UserId,
            Auth = new ResponseRefreshToken
            {
                RefreshToken = refreshTokenFromDb.Value,
                Token = new ResponseToken { AccessToken = newToken }
            }
        };

    }
}
