using CoBudget.Domain.Repositories;
using CoBudget.Domain.Repositories.Expenses;
using CoBudget.Domain.Repositories.Users;
using CoBudget.Domain.Security.Cryptography;
using CoBudget.Infrastructure.DataAccess;
using CoBudget.Infrastructure.DataAccess.Repositories;
using CoBudget.Infrastructure.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CoBudget.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrasctructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);


        services.AddScoped<IPasswordEncripter, PasswordEncripter>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IWorkUnity, WorkUnity>();

        services.AddScoped<IExpensesWriteRepository, ExpensesRepository>();
        services.AddScoped<IExpensesReadRepository, ExpensesRepository>();
        services.AddScoped<IExpenseUpdateRepository, ExpensesRepository>();

        services.AddScoped<IUserReadRepository, UserRepository>();
        services.AddScoped<IUserWriteRepository, UserRepository>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connString = configuration.GetConnectionString("connDbSS");
        services.AddDbContext<CoBudgetDbContext>(config => config.UseSqlServer(connString));
    }
}
