namespace FinanceFlow.Domain.Repositories;

public interface IUnitOfWork
{
    Task Commit();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}