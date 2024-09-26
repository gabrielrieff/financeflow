using FinanceFlow.Domain.Entities;

namespace FinanceFlow.Domain.Repositories.Expenses;

public interface IExpensesUpdateOnlyRepository
{
    Task<Expense?> GetById(User user, long id);
    void Update(Expense expense);
}
