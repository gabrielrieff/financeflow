using FinanceFlow.Domain.Repositories;
using FinanceFlow.Domain.Repositories.Accounts;
using FinanceFlow.Domain.Repositories.Expenses;
using FinanceFlow.Domain.Repositories.Users;
using FinanceFlow.Domain.Security.Cryptography;
using FinanceFlow.Domain.Security.Tokens;
using FinanceFlow.Domain.Services.LoggedUser;
using FinanceFlow.Infrastructure.DataAccess;
using FinanceFlow.Infrastructure.DataAccess.Repositories.Accounts;
using FinanceFlow.Infrastructure.DataAccess.Repositories.Expenses;
using FinanceFlow.Infrastructure.DataAccess.Repositories.Users;
using FinanceFlow.Infrastructure.Extensions;
using FinanceFlow.Infrastructure.Security.Tokens;
using FinanceFlow.Infrastructure.Services.LoggedUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceFlow.Infrastructure;

public static class DependecyInjectionExtension
{

    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
    {
        services.AddScoped<IPasswordEncripter, Security.Cryptography.BCrypt>();
        services.AddScoped<ILoggedUser, LoggedUser>();

        AddRepositories(services);
        AddToken(services, configuration);

        if (configuration.IsTestEnvoriment() == false)
        {
            AddDbContext(services, configuration);
        }
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
        services.AddScoped<IUserUpdateOnlyRepository, UserRepositories>();

        //Expense
        services.AddScoped<IExpensesReadOnlyRepository, ExpensesRepositories>();
        services.AddScoped<IExpensesWhiteOnlyRepository, ExpensesRepositories>();
        services.AddScoped<IExpensesUpdateOnlyRepository, ExpensesRepositories>();

        //Account
        services.AddScoped<IAccountWhiteOnlyRepository, AccountsRepositories>();

    }
    
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("connection");
        var serverVersion = ServerVersion.AutoDetect(connectionString);


        services.AddDbContext<FinanceFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}
