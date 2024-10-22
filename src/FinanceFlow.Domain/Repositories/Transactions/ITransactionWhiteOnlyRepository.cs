using FinanceFlow.Domain.Entities;

namespace FinanceFlow.Domain.Repositories.Transactions;

public interface ITransactionWhiteOnlyRepository
{
    Task Add(Transaction account);
    ///// <summary>
    ///// This function returns TRUE if the deletion was successful
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //Task DeleteById(long id);
}
