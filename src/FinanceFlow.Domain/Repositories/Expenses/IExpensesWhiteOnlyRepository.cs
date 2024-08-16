using FinanceFlow.Domain.Entities;

namespace FinanceFlow.Domain.Repositories.Expenses;

public interface IExpensesWhiteOnlyRepository
{
    Task Add(Expense expense);
    /// <summary>
    /// This function returns TRUE if the deletion was successful
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteById(long id);
}
