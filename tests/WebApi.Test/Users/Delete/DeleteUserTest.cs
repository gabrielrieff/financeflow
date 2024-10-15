using FluentAssertions;
using System.Net;

namespace WebApi.Test.Users.Delete;

public class DeleteUserTest : FinanceFlowClassFixture
{
    private const string Method = "api/User";

    private readonly string _token;

    public DeleteUserTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory._userIdentity.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoDelete(Method, _token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }
}
