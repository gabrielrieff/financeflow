using FinanceFlow.Domain.Repositories.Users;
using Moq;

namespace commonTestUtilities.Repositories.Users;

public class UserWhiteOnlyRepositoryBuilder
{
    public static IUserWhiteOnlyRepository Build()
    {
        var mock = new Mock<IUserWhiteOnlyRepository>();

        return mock.Object;
    }
}
