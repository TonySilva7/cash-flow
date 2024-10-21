using System;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Users.SignIn;

public interface ILoginUserUseCase
{
    Task<ResponseLoginUser> Execute(RequestLoginUser user);
}
