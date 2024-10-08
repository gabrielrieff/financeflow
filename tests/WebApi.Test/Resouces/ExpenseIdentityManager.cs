using FinanceFlow.Domain.Entities;

namespace WebApi.Test.Resouces;

public class ExpenseIdentityManager
{
    private Expense _expense;

    public ExpenseIdentityManager(Expense expense)
    {
        _expense = expense;
    }

    public long GetId() => _expense.Id;

    public DateTime GetDate() => _expense.Create_at;

}
