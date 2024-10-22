using System;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;

namespace CashFlow.Application.UseCases.Users.LoggedUser;

public interface ILoggedUserUseCase
{
    Task<ResponseLoggedUser> Execute();
}
