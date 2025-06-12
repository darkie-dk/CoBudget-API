using CoBudget.Domain.Entities;

namespace CoBudget.Domain.Repositories.Expenses;

public interface IExpensesRepository
{
    Task Add(Expense expense);
    Task<List<Expense>> GetAll();
    Task<Expense?> GetById(long id);
}
