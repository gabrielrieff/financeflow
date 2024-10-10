using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories.Users;
using Moq;

namespace commonTestUtilities.Repositories.Users;

public class UserUpdateOnlyRepositoryBuilder
{
    public static IUserUpdateOnlyRepository Build(User user)
    {
        var mock = new Mock<IUserUpdateOnlyRepository>();

        mock.Setup(repo => repo.GetById(user.Id)).ReturnsAsync(user);

        return mock.Object;
    }
}
