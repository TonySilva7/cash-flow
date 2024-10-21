using System;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception.ExceptionBase;
using Microsoft.Extensions.Options;

namespace CashFlow.Application.UseCases.Users.SignIn;

public class LoginUserUseCase(IUsersRepository usersRepository, IAccessTokenGenerator tokenGenerator, IPasswordEncrypter encrypter) : ILoginUserUseCase
{
    public async Task<ResponseLoginUser> Execute(RequestLoginUser user)
    {
        ValidateLogin(user);

        var userEntity = await usersRepository.LoginAsync(user.Email) ?? throw new UnauthorizedAccessException("Ocorreu um erro ao logar o usuário");
        var allRight = encrypter.Verify(user.Password, userEntity.Password);

        if (!allRight)
        {
            throw new UnauthorizedAccessException("Usuário ou senha inválidos");
        }

        return new ResponseLoginUser
        {
            UserIdentifier = userEntity.UserIdentifier,
            Name = userEntity.Name,
            Email = userEntity.Email,
            Token = new ResponseToken { AccessToken = tokenGenerator.Generate(userEntity.UserIdentifier) }
        };
    }

    private void ValidateLogin(RequestLoginUser user)
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
