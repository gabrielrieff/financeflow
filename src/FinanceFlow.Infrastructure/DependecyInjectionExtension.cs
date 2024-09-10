using FinanceFlow.Domain.Repositories.Expenses;
using FinanceFlow.Domain.Repositories.Users;
using FinanceFlow.Domain.Security.Cryptography;
using FinanceFlow.Infrastructure.DataAccess;
using FinanceFlow.Infrastructure.DataAccess.Repositories.Expenses;
using FinanceFlow.Infrastructure.DataAccess.Repositories.Users;
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

        services.AddScoped<IPassawordEncripter, Security.BCrypt>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        //User
        services.AddScoped<IUserReadOnlyRepository, UserRepositories>();
        services.AddScoped<IUserWhiteOnlyRepository, UserRepositories>();

        //Expense
        services.AddScoped<IExpensesReadOnlyRepository, ExpensesRepositories>();
        services.AddScoped<IExpensesWhiteOnlyRepository, ExpensesRepositories>();
        services.AddScoped<IExpensesUpdateOnlyRepository, ExpensesRepositories>();

    }
    
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("connection");
        var serverVersion = new MySqlServerVersion(new Version(8, 0, 39));


        services.AddDbContext<FinanceFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}
