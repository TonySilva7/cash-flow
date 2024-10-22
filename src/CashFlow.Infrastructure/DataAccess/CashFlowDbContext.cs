using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;
internal class CashFlowDbContext(DbContextOptions<CashFlowDbContext> options) : DbContext(options)
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expense>().ToTable("Tbl_Expenses");
        modelBuilder.Entity<User>().ToTable("Tbl_Users");
        modelBuilder.Entity<RefreshToken>().ToTable("Tbl_RefreshTokens");
    }
}
