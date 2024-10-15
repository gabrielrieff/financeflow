using commonTestUtilities.Requests.User;
using FinanceFlow.Communication.Requests.Login;
using FinanceFlow.Exception.Resource;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Users.changePassword;

public class changePasswordTest : FinanceFlowClassFixture
{
    private const string Method = "api/User/change-password";

    private readonly string _token;
    private readonly string _password;
    private readonly string _email;

    public changePasswordTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory._userIdentity.GetToken();
        _password = webApplicationFactory._userIdentity.GetPassword();
        _email = webApplicationFactory._userIdentity.GetEmail();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestChangePasswordJsonBuilder.Build();
        request.Password = _password;

        var response = await DoPut(Method, request, _token);

        response.StatusCode.Should().Be(HttpStatusCode.NoContent);

        var loginRequest = new RequestLoginJson
        {
            Email = _email,
            Password = _password,
        };

        response = await DoPost("api/Login", loginRequest);
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
        loginRequest.Password = request.NewPassword;
        response = await DoPost("api/Login", loginRequest);
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Error_Password_Different_Current_Password()
    {
        var request = RequestChangePasswordJsonBuilder.Build();

        var result = await DoPut(requestUri: Method, request: request, _token);

        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessage").EnumerateArray();

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(ResourceErrorsMessage.PASSWORD_DIFFERENT_CURRENT_PASSWORD));
    }

}
