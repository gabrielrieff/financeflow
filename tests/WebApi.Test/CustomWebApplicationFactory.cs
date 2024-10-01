using commonTestUtilities.Entities;
using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Security.Cryptography;
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

                StartDatabase(dbContext, passawordEncripter);

            });
    }

    public string GetEmail() => _user.Email;
    public string GetName() => _user.Name;
    public string GetPassword() => _password;

    private void StartDatabase(FinanceFlowDbContext dbContext, IPassawordEncripter passawordEncripter)
    {
        _user = UserBuilder.Build();
        _password = _user.Password;

        _user.Password = passawordEncripter.Encrypt(_user.Password);

        dbContext.Users.Add(_user);
        dbContext.SaveChanges();
    }
}
