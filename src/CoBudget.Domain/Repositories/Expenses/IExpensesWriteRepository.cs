using CoBudget.Domain.Entities;

namespace CoBudget.Domain.Repositories.Expenses;

public interface IExpensesWriteRepository
{
    Task Add(Expense expense);

    /// <summary>
    /// This function returns 'true' if deletion was successful
    /// </summary>
    /// <returns></returns>
    Task<bool> Delete(long id);

}
