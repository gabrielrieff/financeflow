using FinanceFlow.Domain.Security.Cryptography;
using Moq;

namespace commonTestUtilities.Cryptography;

public class PasswordEncripterBuilder
{
    private readonly Mock<IPasswordEncripter> _mock;

    public PasswordEncripterBuilder()
    {
        _mock = new Mock<IPasswordEncripter>();

        _mock.Setup(accessTokenGenerator => accessTokenGenerator.Encrypt(It.IsAny<string>())).Returns("!Password123");
    }

    public PasswordEncripterBuilder Verify(string? password)
    {
        if(string.IsNullOrWhiteSpace(password) == false)
        {
            _mock.Setup(pass => pass.Verify(password, It.IsAny<string>())).Returns(true);
        }

        return this;
    }

    public IPasswordEncripter Build() => _mock.Object;
}
