using FinanceFlow.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceFlow.Infrastructure.Migrations;

public static class DataBaseMigration
{
    public static async Task MigrateDataBase(IServiceProvider serviceProvider)
    {
        var dbContext = serviceProvider.GetRequiredService<FinanceFlowDbContext>();

        await dbContext.Database.MigrateAsync();
    }
}
