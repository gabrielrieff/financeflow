using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories.Expenses;
using Moq;

namespace commonTestUtilities.Repositories.Expenses;

public class ExpensesReadOnlyRepositoryBuilder
{
    private readonly Mock<IExpensesReadOnlyRepository> _repository;

    public ExpensesReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IExpensesReadOnlyRepository>();
    }

    public ExpensesReadOnlyRepositoryBuilder GetAll(FinanceFlow.Domain.Entities.User user, List<Expense> expenses)
    {
        _repository.Setup(repo => repo.GetAll(user)).ReturnsAsync(expenses);
        return this;
    }


    public IExpensesReadOnlyRepository Build() => _repository.Object;

}
