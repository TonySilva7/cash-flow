using System;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.Users;

namespace CashFlow.Application.UseCases.Users.UserVerify;

public class UserVerifyUseCase(IUsersRepository usersRepository) : IUserVerifyUseCase
{

    public async Task<bool> VerifyUser(Guid userIdentifier)
    {
        var exists = await usersRepository.ExistsActiveUserWithIdentifierAsync(userIdentifier);
        return exists;
    }
}
