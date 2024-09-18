using FinanceFlow.Domain.Repositories;
using FinanceFlow.Domain.Repositories.Expenses;
using FinanceFlow.Domain.Repositories.Users;
using FinanceFlow.Domain.Security.Cryptography;
using FinanceFlow.Domain.Security.Tokens;
using FinanceFlow.Infrastructure.DataAccess;
using FinanceFlow.Infrastructure.DataAccess.Repositories.Expenses;
using FinanceFlow.Infrastructure.DataAccess.Repositories.Users;
using FinanceFlow.Infrastructure.Security.Tokens;
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
        AddToken(services, configuration);

        services.AddScoped<IPassawordEncripter, Security.Cryptography.BCrypt>();
    }

    private static void AddToken(IServiceCollection services, IConfiguration configuration)
    {
        var expirationTimeMinutor = configuration.GetValue<uint>("Settings:Jwt:ExpirationMinutes");
        var signingKey = configuration.GetValue<string>("Settings:Jwt:SigningKey");

        services.AddScoped<IAccessTokenGenerator>(config => new JwtTokenGenerator(expirationTimeMinutor, signingKey!));
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
