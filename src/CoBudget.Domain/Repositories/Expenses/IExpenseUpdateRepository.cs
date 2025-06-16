using CoBudget.Domain.Entities;

namespace CoBudget.Domain.Repositories.Expenses;
public interface IExpenseUpdateRepository
{
    Task<Expense?> GetById(long id);
    void Update(Expense expense);

}
