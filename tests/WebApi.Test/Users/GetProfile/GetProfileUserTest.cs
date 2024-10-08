using commonTestUtilities.Entities;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Users.GetProfile;

public class GetProfileUserTest : FinanceFlowClassFixture
{
    private const string Method = "api/User";
    private readonly string _token;
    private readonly string _email;
    private readonly string _name;

    public GetProfileUserTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory._userIdentity.GetToken();
        _name = webApplicationFactory._userIdentity.GetName();
        _email = webApplicationFactory._userIdentity.GetEmail();
    }

    [Fact]
    public async Task Success()
    {
        var user = UserBuilder.Build();

        var result = await DoGet(Method, _token);

        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("name").GetString().Should().Be(_name);
        response.RootElement.GetProperty("email").GetString().Should().Be(_email);


    }
}
