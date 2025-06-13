using CoBudget.Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace CoBudget.Infrastructure.DataAccess.Repositories;

internal class ExpensesRepository : IExpensesReadRepository, IExpensesWriteRepository
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

    public async Task<bool> Delete(long id)
    {
        var result = await _coBudgetDbContext.Expenses.FirstOrDefaultAsync(expense => expense.Id == id);
        if (result is null) return false; 
        
        _coBudgetDbContext.Expenses.Remove(result);

        return true;
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
