using System.Runtime.CompilerServices;
using CoBudget.Domain.Entities;
using CoBudget.Domain.Repositories.Expenses;
namespace CoBudget.Infrastructure.DataAccess.Repositories;

internal class ExpensesRepository : IExpensesRepository
{
    private readonly CoBudgetDbContext _coBudgetDbContext;
    public ExpensesRepository(CoBudgetDbContext dbContext)
    {
        _coBudgetDbContext = dbContext;
    }
    public void Add(Expense expense)
    {

        _coBudgetDbContext.Expenses.Add(expense);
    }
}
