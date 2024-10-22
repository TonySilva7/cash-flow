using System;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Security;

public interface IUserRefreshTokenUseCase
{
    Task<ResponseLoginUser> Execute(RequestRefreshTokenUser request);
}
