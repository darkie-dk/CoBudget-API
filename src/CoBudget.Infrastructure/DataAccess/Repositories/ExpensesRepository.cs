using System.Runtime.CompilerServices;
using CoBudget.Domain.Entities;
using CoBudget.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;
namespace CoBudget.Infrastructure.DataAccess.Repositories;

internal class ExpensesRepository : IExpensesRepository
{
    private readonly CoBudgetDbContext _coBudgetDbContext;
    public ExpensesRepository(CoBudgetDbContext dbContext)
    {
        _coBudgetDbContext = dbContext;
    }
    public async Task Add(Expense expense)
    {

        await _coBudgetDbContext.Expenses.AddAsync(expense);
    }

    public async Task<List<Expense>> GetAll()
    {
        return await _coBudgetDbContext.Expenses.AsNoTracking().ToListAsync();
    }

    public async Task<Expense?> GetById(long id)
    {
        return await _coBudgetDbContext.Expenses.AsNoTracking().FirstOrDefaultAsync(expense => expense.Id == id);
    }
}
