using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories.Transactions;
using Microsoft.EntityFrameworkCore;

namespace FinanceFlow.Infrastructure.DataAccess.Repositories.Transactions;

public class TransactionRepositories : ITransactionWhiteOnlyRepository, ITransactionReadOnlyRepository
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

    public async Task<List<Transaction>> GetMonthByID(List<long> ids)
    {
        return await _dbContext.Transactions
            .Where(t => ids.Contains(t.AccountID))
            .ToListAsync();
    }
}
