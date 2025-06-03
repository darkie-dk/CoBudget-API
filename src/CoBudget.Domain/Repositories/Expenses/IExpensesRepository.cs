using CoBudget.Domain.Entities;

namespace CoBudget.Domain.Repositories.Expenses;

public interface IExpensesRepository
{
    void Add(Expense expense);
}
