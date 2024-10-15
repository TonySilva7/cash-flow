using CashFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Infrastructure.DataAccess;
public class CashFlowDbContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = "Server=192.168.0.237;Database=cashflow_db;User ID=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True";
        optionsBuilder.UseSqlServer(connectionString);
    }
}
