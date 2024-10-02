using FinanceFlow.Domain.Repositories.Expenses;
using Moq;

namespace commonTestUtilities.Repositories.Expenses;

public class ExpensesWhiteOnlyRepositoryBuilder
{
    public static IExpensesWhiteOnlyRepository Build()
    {
        var mock = new Mock<IExpensesWhiteOnlyRepository>();

        return mock.Object;
    }
}
