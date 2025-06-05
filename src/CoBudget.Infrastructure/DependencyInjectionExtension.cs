using CoBudget.Domain.Repositories.Expenses;
using CoBudget.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CoBudget.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrasctructure(this IServiceCollection services)
    {
        services.AddScoped<IExpensesRepository, ExpensesRepository>();
    }
}
