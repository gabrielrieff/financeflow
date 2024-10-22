using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories.Accounts;
using FinanceFlow.Domain.Repositories.Reccurences;

namespace FinanceFlow.Infrastructure.DataAccess.Repositories.Reccurences;

public class ReccurenceRepositories : IReccurenceWhiteOnlyRepository
{
    private readonly FinanceFlowDbContext _dbContext;

    public ReccurenceRepositories(FinanceFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Recurrence reccurence)
    {
        await _dbContext.Recurrences.AddAsync(reccurence);
    }
}
