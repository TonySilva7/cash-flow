using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.Tokens;
using CashFlow.Domain.Security.Cryptography;
using CashFlow.Domain.Security.Tokens;
using CashFlow.Infrastructure.DataAccess;
using CashFlow.Infrastructure.DataAccess.Repositories;
using CashFlow.Infrastructure.Security.Cryptography;
using CashFlow.Infrastructure.Security.Tokens.Access.Generator;
using CashFlow.Infrastructure.Security.Tokens.Access.Validator;
using CashFlow.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfraStructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
        AddTokens(services, configuration);
        AddSecurity(services);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IExpensesRepository, ExpensesRepository>();
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<CashFlowDbContext>((config) => config.UseSqlServer(connectionString));
    }

    private static void AddSecurity(IServiceCollection services)
    {
        services.AddScoped<IPasswordEncrypter, BCryptNet>();
    }


    private static void AddTokens(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeInMinutes = configuration.GetValue<uint>("Jwt:ExpirationTimeInMinutes");
        var signingKey = configuration.GetValue<string>("Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator, JwtTokenGenerator>(option => new JwtTokenGenerator(expirationTimeInMinutes, signingKey!));
        services.AddScoped<IAccessTokenValidator, JwtTokenValidator>(option => new JwtTokenValidator(signingKey!));
        services.AddScoped<IUserIdentifierProvider, LoggedUserIdentifier>();
    }
}
