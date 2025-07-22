using CoBudget.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CoBudget.Infrastructure.DataAccess;
internal class CoBudgetDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<User> Users { get; set; }
}
