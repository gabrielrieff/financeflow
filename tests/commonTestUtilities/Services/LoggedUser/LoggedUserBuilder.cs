using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Services.LoggedUser;
using Moq;

namespace commonTestUtilities.Services.LoggedUser;

public class LoggedUserBuilder
{
    public static ILoggedUser Build(User user)
    {
        var mock = new Mock<ILoggedUser>();

        mock.Setup(logged => logged.Get()).ReturnsAsync(user);

        return mock.Object;
    }
}
