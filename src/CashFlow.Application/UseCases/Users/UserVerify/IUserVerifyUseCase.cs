using System;

namespace CashFlow.Application.UseCases.Users.UserVerify;

public interface IUserVerifyUseCase
{
    Task<bool> VerifyUser(Guid userIdentifier);
}
