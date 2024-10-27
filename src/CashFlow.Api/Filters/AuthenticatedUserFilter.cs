using System;
using System.Security.Claims;
using CashFlow.Application.UseCases.Users.UserVerify;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Security.Tokens;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;

namespace CashFlow.Api.Filters;

public class AuthenticatedUserFilter(IAccessTokenValidator accessTokenValidator, IUserVerifyUseCase verifyUseCase, string[] requiredRole) : IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        try
        {
            var token = GetTokenOnRequest(context);

            (var userIdentifier, var roles) = accessTokenValidator.ValidateAndGetUserIdentifier(token);
            var exists = await verifyUseCase.VerifyUser(userIdentifier);

            if (!exists)
            {
                throw new UnauthorizedAccessException("Usuário não encontrado!");
            }

            if (requiredRole.Length > 0 && roles != null && !roles.Intersect(requiredRole).Any())
            {
                context.Result = new ObjectResult(new ResponseError("Usuário não autorizado!")) { StatusCode = 403 };
            }
        }
        catch (SecurityTokenExpiredException)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseError("TokenIsExpired") { TokenIsExpired = true });
        }
        catch (UnauthorizedAccessException ex)
        {
            context.Result = new UnauthorizedObjectResult(new ResponseError(ex.Message));
        }
        catch
        {
            context.Result = new UnauthorizedObjectResult(new ResponseError("Erro ao autenticar usuário"));
        }

    }

    private static string GetTokenOnRequest(AuthorizationFilterContext context)
    {
        var authentication = context.HttpContext.Request.Headers.Authorization.ToString();

        if (string.IsNullOrEmpty(authentication))
        {
            throw new UnauthorizedAccessException("Usuário não autorizado!");
        }

        // recorta o token sem o prefixo "Bearer " e remove espaços em branco
        return authentication["Bearer ".Length..].Trim();
    }
}
