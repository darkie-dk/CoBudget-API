using CoBudget.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoBudget.Infrastructure.DataAccess;
public class CoBudgetDbContext : DbContext
{
    public CoBudgetDbContext(DbContextOptions<CoBudgetDbContext> options)
       : base(options)
    {
    }

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
