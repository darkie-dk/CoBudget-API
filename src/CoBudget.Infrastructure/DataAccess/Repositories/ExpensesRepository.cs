using CoBudget.Domain.Entities;
using CoBudget.Domain.Repositories.Expenses;

namespace CoBudget.Infrastructure.DataAccess.Repositories;

internal class ExpensesRepository : IExpensesRepository
{
    public void Add(Expense expense)
    {
        var dbContext = new CoBudgetDbContext();

        dbContext.Expenses.Add(expense);

        dbContext.SaveChanges();
    }

}
