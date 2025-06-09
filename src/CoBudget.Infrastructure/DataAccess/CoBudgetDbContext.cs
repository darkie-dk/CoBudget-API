using CoBudget.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoBudget.Infrastructure.DataAccess;
public class CoBudgetDbContext : DbContext
{
    public CoBudgetDbContext(DbContextOptions options) : base(options) { }
    public DbSet<Expense> Expenses { get; set; }
}
