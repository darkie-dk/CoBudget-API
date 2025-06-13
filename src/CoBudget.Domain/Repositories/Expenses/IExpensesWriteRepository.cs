using CoBudget.Domain.Entities;

namespace CoBudget.Infrastructure.DataAccess.Repositories;

public interface IExpensesWriteRepository
{
    Task Add(Expense expense);

}
