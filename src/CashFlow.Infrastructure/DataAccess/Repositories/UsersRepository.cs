using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess.Repositories;
internal class UsersRepository(CashFlowDbContext dbContext) : IUsersRepository
{
    public async Task AddAsync(User user) => await dbContext.Users.AddAsync(user);
    public async Task<bool> ExistsActiveUserWithIdentifierAsync(Guid identifier) => await dbContext.Users.AsNoTracking().AnyAsync(user => user.UserIdentifier == identifier && user.FlActive);

    public Task<User?> GetByIdentifierAsync(Guid id) => dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.UserIdentifier == id);

    public async Task<User?> LoginAsync(string email) => await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email == email);
}
