using CoBudget.Domain.Repositories;

namespace CoBudget.Infrastructure.DataAccess;

internal class WorkUnity : IWorkUnit
{
    private readonly CoBudgetDbContext _coBudgetDbContext;
    public WorkUnity(CoBudgetDbContext dbContext)
    {
        _coBudgetDbContext = dbContext;
    }
    public async Task Commit() => await _coBudgetDbContext.SaveChangesAsync();
}
