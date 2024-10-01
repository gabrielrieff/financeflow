using FinanceFlow.Communication.Requests.Login;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace WebApi.Test.Login.DoLogin;

public class DoLoginTest : IClassFixture<CustomWebApplicationFactory>
{
    private const string Method = "api/Login";

    private readonly HttpClient _httpClient;
    private string _email;
    private string _name;
    private string _password;

    public DoLoginTest(CustomWebApplicationFactory webApplicationFactory)
    {
        _httpClient = webApplicationFactory.CreateClient();
        _email = webApplicationFactory.GetEmail();
        _name = webApplicationFactory.GetName();
        _password = webApplicationFactory.GetPassword();

    }

    [Fact]
    public async Task Success()
    {
        var request = new RequestLoginJson
        {
            Email = _email,
            Password = _password
        };

        var result = await _httpClient.PostAsJsonAsync(Method, request);

        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("name").GetString().Should().Be(_name);
        response.RootElement.GetProperty("token").GetString().Should().NotBeNullOrEmpty();

    }

    public async Task Error_Empty_Name()
    {
    }
}
