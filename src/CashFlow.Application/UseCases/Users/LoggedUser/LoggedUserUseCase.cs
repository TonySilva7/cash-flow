using System;
using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.Users.LoggedUser;

public class LoggedUserUseCase(IUserIdentifierProvider identifierProvider, IUsersRepository usersRepository, IMapper mapper) : ILoggedUserUseCase
{
    public async Task<ResponseLoggedUser> Execute()
    {
        var userIdentifier = identifierProvider.GetUserIdentifier();
        var user = await usersRepository.GetByIdentifierAsync(userIdentifier) ?? throw new NotFoundException("User not found");

        return mapper.Map<ResponseLoggedUser>(user);
    }
}
