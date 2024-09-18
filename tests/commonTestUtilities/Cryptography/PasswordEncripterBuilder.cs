using FinanceFlow.Domain.Security.Cryptography;
using Moq;

namespace commonTestUtilities.Cryptography;

public class PasswordEncripterBuilder
{
    public static IPassawordEncripter Build()
    {
        var mock = new Mock<IPassawordEncripter>();

        mock.Setup(accessTokenGenerator => accessTokenGenerator.Encrypt(It.IsAny<string>())).Returns("!Password123");

        return mock.Object;
    }
}
