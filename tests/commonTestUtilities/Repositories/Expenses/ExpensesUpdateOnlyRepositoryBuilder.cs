using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories.Expenses;
using Moq;

namespace commonTestUtilities.Repositories.Expenses;

public class ExpensesUpdateOnlyRepositoryBuilder
{

    private readonly Mock<IExpensesUpdateOnlyRepository> _repository;
    public ExpensesUpdateOnlyRepositoryBuilder()
    {
        _repository = new Mock<IExpensesUpdateOnlyRepository>();
    }

    public ExpensesUpdateOnlyRepositoryBuilder GetById(User user, Expense? expenses)
    {
        if (expenses is not null)
            _repository.Setup(repo => repo.GetById(user, expenses.Id)).ReturnsAsync(expenses);

        return this;
    }

    public IExpensesUpdateOnlyRepository Build() => _repository.Object;

}
