using FinanceFlow.Domain.Repositories;
using FinanceFlow.Domain.Repositories.Users;
using Moq;

namespace commonTestUtilities.Repositories.User;

public class UserWhiteOnlyRepositoryBuilder
{
    public static IUserWhiteOnlyRepository Build()
    {
        var mock = new Mock<IUserWhiteOnlyRepository>();

        return mock.Object;
    }
}
