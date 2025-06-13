using CoBudget.Domain.Entities;

    namespace CoBudget.Infrastructure.DataAccess.Repositories;

public interface IExpensesReadRepository
{
    Task<List<Expense>> GetAll();
    Task<Expense?> GetById(long id);
}
