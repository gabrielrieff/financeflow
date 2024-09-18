
using FinanceFlow.Domain.Repositories;

namespace FinanceFlow.Infrastructure.DataAccess;

internal class UnitOfWork : IUnitOfWork
{

    public FinanceFlowDbContext _dbContext;

    public UnitOfWork(FinanceFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task Commit() => await _dbContext.SaveChangesAsync();
}
