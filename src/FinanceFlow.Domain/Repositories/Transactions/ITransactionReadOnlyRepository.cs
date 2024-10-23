using FinanceFlow.Domain.Entities;

namespace FinanceFlow.Domain.Repositories.Transactions;
public interface ITransactionReadOnlyRepository
{
    Task<List<Transaction>> GetMonthByID(List<long> ids);
}
