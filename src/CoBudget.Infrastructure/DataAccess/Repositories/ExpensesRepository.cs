using CoBudget.Domain.Entities;
using CoBudget.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;
namespace CoBudget.Infrastructure.DataAccess.Repositories;

internal class ExpensesRepository(CoBudgetDbContext context) : IExpensesReadRepository, IExpensesWriteRepository, IExpenseUpdateRepository
{
    private readonly CoBudgetDbContext _context = context;

    public async Task Add(Expense expense)
    {

        await _context.Expenses.AddAsync(expense);
    }

    public async Task<bool> Delete(long id)
    {
        var result = await _context.Expenses.FirstOrDefaultAsync(expense => expense.Id == id);
        if (result is null) return false;

        _context.Expenses.Remove(result);

        return true;
    }

    public async Task<List<Expense>> GetAll()
    {
        return await _context.Expenses.AsNoTracking().ToListAsync();
    }

    async Task<Expense?> IExpensesReadRepository.GetById(long id)
    {
        return await _context.Expenses.AsNoTracking().FirstOrDefaultAsync(expense => expense.Id == id);
    }

    async Task<Expense?> IExpenseUpdateRepository.GetById(long id)
    {
        return await _context.Expenses.FirstOrDefaultAsync(expense => expense.Id == id);
    }

    public void Update(Expense expense)
    {
        _context.Expenses.Update(expense);
    }

    public async Task<List<Expense>> FilterByMonth(DateOnly date)
    { 
        var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;

        var endDayDate = DateTime.DaysInMonth(year: date.Year, month: date.Month);
        var endDate = new DateTime(year: date.Year, month: date.Month, day: endDayDate, hour: 23, minute: 59, second: 59);

        return await _context
            .Expenses
            .AsNoTracking()
            .Where(expense => expense.Date >= startDate && expense.Date <= endDate)
            .OrderBy(expense => expense.Date)
            .ToListAsync();
    }
}
