using commonTestUtilities.Requests.User;
using FinanceFlow.Communication.Requests.Login;
using FinanceFlow.Exception.Resource;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Login.DoLogin;

public class DoLoginTest : FinanceFlowClassFixture
{
    private const string Method = "api/Login";

    private string _email;
    private string _name;
    private string _password;

    public DoLoginTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _email = webApplicationFactory._userIdentity.GetEmail();
        _name = webApplicationFactory._userIdentity.GetName();
        _password = webApplicationFactory._userIdentity.GetPassword();
    }

    [Fact]
    public async Task Success()
    {
        var request = new RequestLoginJson
        {
            Email = _email,
            Password = _password
        };

        var result = await DoPost(requestUri: Method, request: request);

        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("name").GetString().Should().Be(_name);
        response.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Error_Validate_Login()
    {
        var request = RequestLoginJsonBuilder.Build();

        var result = await DoPost(requestUri: Method, request: request);

        result.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessage").EnumerateArray();

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(ResourceErrorsMessage.EMAIL_OR_PASSWORD_INVALID));

    }
}
