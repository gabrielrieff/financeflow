using FinanceFlow.Domain.Repositories.Expenses;
using FinanceFlow.Infrastructure.DataAccess;
using FinanceFlow.Infrastructure.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceFlow.Infrastructure;

public static class DependecyInjectionExtension
{

    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IExpensesReadOnlyRepository, ExpensesRepositorioes>();
        services.AddScoped<IExpensesWhiteOnlyRepository, ExpensesRepositorioes>();
        services.AddScoped<IExpensesUpdateOnlyRepository, ExpensesRepositorioes>();
    }
    
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("connection");
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 39));


        services.AddDbContext<FinanceFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}
