using commonTestUtilities.Entities;
using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Security.Cryptography;
using FinanceFlow.Domain.Security.Tokens;
using FinanceFlow.Infrastructure.DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Test.Resouces;

namespace WebApi.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    public UserIdentityManager _userIdentity { get; private set; } = default!;
    public AccountIdentityManager _accountIdentity {  get; private set; } = default!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<FinanceFlowDbContext>(config =>
                {
                    config.UseInMemoryDatabase("InMemoryDbForTesting");
                    config.UseInternalServiceProvider(provider);
                });

                var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<FinanceFlowDbContext>();
                var passwordEncripter = scope.ServiceProvider.GetRequiredService<IPasswordEncripter>();
                var accessTokenGenerator = scope.ServiceProvider.GetRequiredService<IAccessTokenGenerator>();

                StartDatabase(dbContext, passwordEncripter, accessTokenGenerator);
            });
    }


    private void StartDatabase(
        FinanceFlowDbContext dbContext, 
        IPasswordEncripter passwordEncripter, 
        IAccessTokenGenerator AccessTokenGenerator)
    {
        var user = AddUser(dbContext, passwordEncripter, AccessTokenGenerator);
        AddAccount(dbContext, user, accountId: 2, tagId: 2);

        dbContext.SaveChanges();
    }

    private User AddUser(
        FinanceFlowDbContext dbContext, 
        IPasswordEncripter passawordEncripter, 
        IAccessTokenGenerator AccessTokenGenerator)
    {
        var user = UserBuilder.Build();
        var password = user.Password;

        user.Password = passawordEncripter.Encrypt(user.Password);

        dbContext.Users.Add(user);

        var token = AccessTokenGenerator.Generate(user);

        _userIdentity = new UserIdentityManager(user, password, token);

        return user;
    }

    private void AddAccount(FinanceFlowDbContext dbContext, User user, long accountId, long tagId)
    {
        var account = AccountBuilder.Build(user);
        account.ID = accountId;

        foreach (var tag in account.Tags)
        {
            tag.AccountId = accountId;
            tag.Id = tagId;
        };

        dbContext.Accounts.Add(account);

        _accountIdentity = new AccountIdentityManager(account);
    }

}
