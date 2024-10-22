using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories.Transactions;

namespace FinanceFlow.Infrastructure.DataAccess.Repositories.Transactions;

public class TransactionRepositories : ITransactionWhiteOnlyRepository
{
    private readonly FinanceFlowDbContext _dbContext;

    public TransactionRepositories(FinanceFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Transaction transaction)
    {
        await _dbContext.Transactions.AddAsync(transaction);
    }
}
