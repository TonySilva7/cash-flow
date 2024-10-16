using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;
internal class CashFlowDbContext(DbContextOptions<CashFlowDbContext> options) : DbContext(options)
{
    public DbSet<Expense> Expenses { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Expense>().ToTable("Tbl_Expenses");
    }
}
