using commonTestUtilities.Requests.Expense;
using FinanceFlow.Exception.Resource;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Expenses.Register;

public class RegisterExpenseTest : FinanceFlowClassFixture
{
    private const string Method = "api/Expenses";

    private readonly string _token;

    public RegisterExpenseTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory.GetToken();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestExpensesJsonBuilder.Build();

        var result = await DoPost(requestUri: Method, request: request, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.Created);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("title").GetString().Should().Be(request.Title);
    }

    [Fact]
    public async Task Error_Title_Empty()
    {
        var request = RequestExpensesJsonBuilder.Build();
        request.Title = string.Empty;

        var result = await DoPost(requestUri: Method, request: request, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessage").EnumerateArray();

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(ResourceErrorsMessage.TITLE_REQUIRED));
    }

}
