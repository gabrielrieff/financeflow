using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Security.Tokens;
using Moq;

namespace commonTestUtilities.Token;

public class JwtTokenGeneratorBuilder
{

    public static IAccessTokenGenerator Build()
    {
        var mock = new Mock<IAccessTokenGenerator>();

        mock.Setup(accessTokenGenerator => accessTokenGenerator.Generate(It.IsAny<User>())).Returns("Token");

        return mock.Object;
    }
}
