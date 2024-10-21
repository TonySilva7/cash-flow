using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IUsersRepository
{
    Task AddAsync(User user);
}
