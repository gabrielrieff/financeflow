using FinanceFlow.Domain.Repositories;
using FinanceFlow.Domain.Repositories.Accounts;
using FinanceFlow.Domain.Repositories.Reccurences;
using FinanceFlow.Domain.Repositories.Recurrences;
using FinanceFlow.Domain.Repositories.Transactions;
using FinanceFlow.Domain.Repositories.Users;
using FinanceFlow.Domain.Security.Cryptography;
using FinanceFlow.Domain.Security.Tokens;
using FinanceFlow.Domain.Services.LoggedUser;
using FinanceFlow.Infrastructure.DataAccess;
using FinanceFlow.Infrastructure.DataAccess.Repositories.Accounts;
using FinanceFlow.Infrastructure.DataAccess.Repositories.Reccurences;
using FinanceFlow.Infrastructure.DataAccess.Repositories.Transactions;
using FinanceFlow.Infrastructure.DataAccess.Repositories.Users;
using FinanceFlow.Infrastructure.Extensions;
using FinanceFlow.Infrastructure.Security.Tokens;
using FinanceFlow.Infrastructure.Services.LoggedUser;
using FinanceFlow.Infrastructure.Services.SendMail;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mail;
using System.Net;
using FinanceFlow.Domain.Services.SendMail;
using FinanceFlow.Domain.Services.CodeHash;
using FinanceFlow.Infrastructure.Services.CodeHash;

namespace FinanceFlow.Infrastructure;

public static class DependecyInjectionExtension
{

    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
    {
        services.AddScoped<IPasswordEncripter, Security.Cryptography.BCrypt>();
        services.AddScoped<ILoggedUser, LoggedUser>();

        AddRepositories(services);
        AddToken(services, configuration);
        ConfigureServices(services, configuration);

        if (configuration.IsTestEnvoriment() == false)
        {
            AddDbContext(services, configuration);
        }
    }

    private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {

        var Host = configuration.GetValue<string>("Settings:Smtp:Host");
        var Port = configuration.GetValue<int>("Settings:Smtp:Port");
        var Username = configuration.GetValue<string>("Settings:Smtp:Username");
        var Password = configuration.GetValue<string>("Settings:Smtp:Password");
        var EnableSsl = configuration.GetValue<bool>("Settings:Smtp:EnableSsl");


        services.AddScoped(provider =>
        {
            var smtpClient = new SmtpClient(Host)
            {
                Port = Port,
                Credentials = new NetworkCredential(Username, Password),
                EnableSsl = EnableSsl,
                Timeout = 50000,
                UseDefaultCredentials = false,
            };
            return smtpClient;
        });
        services.AddScoped<IEmailService, EmailService>();
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

        //Account
        services.AddScoped<IAccountWhiteOnlyRepository, AccountRepositories>();
        services.AddScoped<IAccountsReadOnlyRepository, AccountRepositories>();
        services.AddScoped<IAccountUpdateOnlyRepository, AccountRepositories>();

        //Reccurence
        services.AddScoped<IRecurrenceWhiteOnlyRepository, ReccurenceRepositories>();
        services.AddScoped<IRecurrenceReadOnlyRepository, ReccurenceRepositories>();
        services.AddScoped<IRecurrenceUpdateOnlyRepository, ReccurenceRepositories>();


        //Transaction
        services.AddScoped<ITransactionWhiteOnlyRepository, TransactionRepositories>();
        services.AddScoped<ITransactionReadOnlyRepository, TransactionRepositories>();

        services.AddScoped<ICodeHash, CodeHash>();
    }
    
    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {

        var connectionString = configuration.GetConnectionString("connection");
        var serverVersion = ServerVersion.AutoDetect(connectionString);


        services.AddDbContext<FinanceFlowDbContext>(config => config.UseMySql(connectionString, serverVersion));
    }
}
