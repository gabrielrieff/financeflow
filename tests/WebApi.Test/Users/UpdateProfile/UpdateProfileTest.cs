using commonTestUtilities.Requests.User;
using FinanceFlow.Exception.Resource;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Users.UpdateProfile;

public class UpdateProfileTest : FinanceFlowClassFixture
{
    private const string Method = "api/User";
    private readonly string _token;


    public UpdateProfileTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory._userIdentity.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestUpdateProfileJsonBuilder.Build();

        var result = await DoPut(requestUri: Method, request: request, _token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Error_Empty_Name()
    {
        var request = RequestUpdateProfileJsonBuilder.Build();
        request.Name = string.Empty;

        var result = await DoPut(requestUri: Method, request: request, _token);

        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessage").EnumerateArray();

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(ResourceErrorsMessage.NAME_REQUIRED));
    }

}
