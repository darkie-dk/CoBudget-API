using CoBudget.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CoBudget.Infrastructure.Migrations;

public static class DataBaseMigration
{
    public async static Task MigrateDatabase(IServiceProvider serviceProvider)
    {
        var dbcontext = serviceProvider.GetRequiredService<CoBudgetDbContext>();
        await dbcontext.Database.MigrateAsync();
    }
}

