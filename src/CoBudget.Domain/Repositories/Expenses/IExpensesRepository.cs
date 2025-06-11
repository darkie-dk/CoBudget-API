using CoBudget.Domain.Entities;

namespace CoBudget.Domain.Repositories.Expenses;

public interface IExpensesRepository
{
    Task Add(Expense expense);
}
