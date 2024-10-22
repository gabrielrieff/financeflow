using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories.Accounts;

namespace FinanceFlow.Infrastructure.DataAccess.Repositories.Accounts;

public class AccountRepositories : IAccountWhiteOnlyRepository
{
    private readonly FinanceFlowDbContext _dbContext;

    public AccountRepositories(FinanceFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Account> Add(Account account)
    {
        var result = await _dbContext.Accounts.AddAsync(account);

        return result.Entity;
    }
}
