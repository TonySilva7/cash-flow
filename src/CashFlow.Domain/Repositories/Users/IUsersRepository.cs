using CashFlow.Domain.Entities;

namespace CashFlow.Domain.Repositories.Expenses;
public interface IUsersRepository
{
    Task AddAsync(User user);
    Task<bool> ExistsActiveUserWithIdentifierAsync(Guid identifier);
    Task<User?> LoginAsync(string email);
    Task<User?> GetByIdentifierAsync(Guid id);
}
