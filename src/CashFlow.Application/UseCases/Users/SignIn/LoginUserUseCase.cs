using System;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.Tokens;
using CashFlow.Domain.Repositories.Users;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception.ExceptionBase;
using Microsoft.Extensions.Options;

namespace CashFlow.Application.UseCases.Users.SignIn;

public class LoginUserUseCase(
    IUsersRepository usersRepository,
    IAccessTokenGenerator tokenGenerator,
    IPasswordEncrypter encrypter,
    IRefreshTokenRepository refreshTokenRepository,
    IUnitOfWork unitOfWork) : ILoginUserUseCase
{
    public async Task<ResponseLoginUser> Execute(RequestLoginUser user)
    {
        ValidateLogin(user);

        var userEntity = await usersRepository.LoginAsync(user.Email) ?? throw new UnauthorizedAccessException("Ocorreu um erro ao logar o usuário");
        var allRight = encrypter.IsValidPassword(user.Password, userEntity.Password);

        if (!allRight)
        {
            throw new UnauthorizedAccessException("Usuário ou senha inválidos");
        }

        var refreshValue = tokenGenerator.GenerateRefreshToken();
        var tokenValue = tokenGenerator.Generate(userEntity.UserIdentifier);

        var refreshToken = new RefreshToken
        {
            UserId = userEntity.Id,
            Value = refreshValue
        };

        await refreshTokenRepository.SaveRefreshTokenAsync(refreshToken);
        await unitOfWork.Commit();

        return new ResponseLoginUser
        {
            UserIdentifier = userEntity.UserIdentifier,
            Auth = new ResponseRefreshToken
            {
                RefreshToken = refreshToken.Value,
                Token = new ResponseToken { AccessToken = tokenValue }
            }
        };
    }

    private static void ValidateLogin(RequestLoginUser user)
    {
        var validator = new LoginUserValidator();
        var result = validator.Validate(user);

        if (!result.IsValid)
        {
            var errorsMessage = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorsMessage);
        }
    }
}
