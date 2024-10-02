using commonTestUtilities.Entities;
using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Security.Cryptography;
using FinanceFlow.Domain.Security.Tokens;
using FinanceFlow.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private User _user;
    private string _password;
    private string _token;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<FinanceFlowDbContext>(config =>
                {
                    config.UseInMemoryDatabase("InMemoryDbForTesting");
                    config.UseApplicationServiceProvider(provider);
                });

                var scoped = services.BuildServiceProvider().CreateScope();
                var dbContext = scoped.ServiceProvider.GetRequiredService<FinanceFlowDbContext>();
                var passawordEncripter = scoped.ServiceProvider.GetRequiredService<IPassawordEncripter>();
                var tokenGenerator = scoped.ServiceProvider.GetService<IAccessTokenGenerator>();

                StartDatabase(dbContext, passawordEncripter);

                _token = tokenGenerator.Generate(_user);
            });
    }

    public string GetEmail() => _user.Email;
    public string GetName() => _user.Name;
    public string GetPassword() => _password;
    public string GetToken() => _token;

    private void StartDatabase(FinanceFlowDbContext dbContext, IPassawordEncripter passawordEncripter)
    {
        AddUsers(dbContext, passawordEncripter);
        AddExpenses(dbContext, _user);
        dbContext.SaveChanges();
    }

    private void AddUsers(FinanceFlowDbContext dbContext, IPassawordEncripter passawordEncripter)
    {
        _user = UserBuilder.Build();
        _password = _user.Password;

        _user.Password = passawordEncripter.Encrypt(_user.Password);

        dbContext.Users.Add(_user);
    }

    private void AddExpenses(FinanceFlowDbContext dbContext, User user)
    {
        var expense = ExpenseBuilder.Build(user);

        dbContext.Expenses.Add(expense);
    }
}
