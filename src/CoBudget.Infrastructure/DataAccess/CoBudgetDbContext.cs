using CoBudget.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoBudget.Infrastructure.DataAccess;
internal class CoBudgetDbContext : DbContext
{
    public DbSet<Expense> Expenses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connString = "Server=localhost;Database=cobudgetDb;Trusted_Connection=True;TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(connString);
        }
    }
}
