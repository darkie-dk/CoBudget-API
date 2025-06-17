using CoBudget.Domain.Entities;

namespace CoBudget.Domain.Repositories.Expenses;

public interface IExpensesReadRepository
{
    Task<List<Expense>> GetAll();
    Task<Expense?> GetById(long id);
    Task<List<Expense>> FilterByMonth(DateOnly date);

}
