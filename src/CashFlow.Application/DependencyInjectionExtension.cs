using CashFlow.Application.AutoMapper;
using CashFlow.Application.UseCases.Expenses;
using CashFlow.Application.UseCases.Expenses.GetById;
using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Application.UseCases.Security;
using CashFlow.Application.UseCases.Users.LoggedUser;
using CashFlow.Application.UseCases.Users.Register;
using CashFlow.Application.UseCases.Users.SignIn;
using CashFlow.Application.UseCases.Users.UserVerify;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddAutoMapper(services);
        AddUseCases(services);
    }

    private static void AddAutoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(AutoMapping));
    }

    private static void AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IRegisterExpenseUseCase, RegisterExpenseUseCase>();
        services.AddScoped<IGetAllExpensesUseCase, GetAllExpensesUseCase>();
        services.AddScoped<IGetExpenseByIdUseCase, GetExpenseByIdUseCase>();
        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IUserVerifyUseCase, UserVerifyUseCase>();
        services.AddScoped<ILoginUserUseCase, LoginUserUseCase>();
        services.AddScoped<ILoggedUserUseCase, LoggedUserUseCase>();
        services.AddScoped<IUserRefreshTokenUseCase, UserRefreshTokenUseCase>();
    }
}
