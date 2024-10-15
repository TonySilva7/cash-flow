using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Infrastructure;

public static class DependencyInjectionExtension
{
  public static void AddInfraStructure(this IServiceCollection services)
  {
    services.AddScoped<IExpensesRepository, ExpensesRepository>();
  }
}
