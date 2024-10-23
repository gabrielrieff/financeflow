using FinanceFlow.Domain.Entities;

namespace FinanceFlow.Domain.Repositories.Accounts;

public interface IAccountWhiteOnlyRepository
{
    Task<Account> Add(Account account);
    /// <summary>
    /// This function returns TRUE if the deletion was successful
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteById(long id);
}
