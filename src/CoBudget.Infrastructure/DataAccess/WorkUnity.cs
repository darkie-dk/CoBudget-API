using CoBudget.Domain.Repositories;

namespace CoBudget.Infrastructure.DataAccess;

internal class WorkUnity : IWorkUnity
{
    private readonly CoBudgetDbContext _coBudgetDbContext;
    public WorkUnity(CoBudgetDbContext dbContext)
    {
        _coBudgetDbContext = dbContext;
    }
    public void Commit() => _coBudgetDbContext.SaveChanges();
}
