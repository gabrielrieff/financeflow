using FinanceFlow.Domain.Repositories.Accounts;
using Moq;

namespace commonTestUtilities.Repositories.Accounts;

public class AccountsWhiteOnlyRepositoryBuilder
{
    public static IAccountWhiteOnlyRepository Build()
    {
        var mock = new Mock<IAccountWhiteOnlyRepository>();

        return mock.Object;
    }
}
