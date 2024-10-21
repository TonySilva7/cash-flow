using System;
using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Users.Register;

public class RegisterUserUseCase(
    IUsersRepository usersRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper,
    IAccessTokenGenerator jwtTokenGenerator,
    IPasswordEncrypter encrypter) : IRegisterUserUseCase
{

    public async Task<ResponseRegisteredUser> Execute(RequestRegisterUser userRequest)
    {
        Validate(userRequest);

        var userEntity = mapper.Map<User>(userRequest);

        userEntity.Password = encrypter.Encrypt(userRequest.Password);
        userEntity.UserIdentifier = Guid.NewGuid(); // identificador do usuário para o token

        await usersRepository.AddAsync(userEntity);
        await unitOfWork.Commit();

        var response = mapper.Map<ResponseRegisteredUser>(userEntity);
        response.Token = new ResponseToken { AccessToken = jwtTokenGenerator.Generate(userEntity.UserIdentifier) };

        return response;
    }

    private static void Validate(RequestRegisterUser user)
    {
        var validator = new RegisterUserValidator();
        var result = validator.Validate(user);

        if (!result.IsValid)
        {
            var errorsMessage = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorsMessage);
        }
    }
}
