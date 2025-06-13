using CoBudget.Domain.Entities;

namespace CoBudget.Infrastructure.DataAccess.Repositories;

public interface IExpensesWriteRepository
{
    Task Add(Expense expense);
    /// <summary>
    /// This function returns 'true' if deletion was successful
    /// </summary>
    /// <returns></returns>
    Task<bool> Delete(long id);

}
