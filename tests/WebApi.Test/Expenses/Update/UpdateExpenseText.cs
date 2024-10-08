using commonTestUtilities.Requests.Expense;
using FinanceFlow.Exception.Resource;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Expenses.Update;

public class UpdateExpenseText : FinanceFlowClassFixture
{
    private const string Method = "api/Expenses";

    private readonly string _token;
    private readonly long _expenseId;

    public UpdateExpenseText(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory._userIdentity.GetToken();
        _expenseId = webApplicationFactory._expenseIdentity.GetId();
    }

    [Fact]
    public async Task Success()
    {
        var request = RequestExpensesJsonBuilder.Build();

        var result = await DoPut(requestUri: $"{Method}/{_expenseId}", request: request, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);
    }

    [Fact]
    public async Task Error_Title_Empty()
    {
        var request = RequestExpensesJsonBuilder.Build();
        request.Title = string.Empty;

        var result = await DoPut(requestUri: $"{Method}/{_expenseId}", request: request, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessage").EnumerateArray();

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(ResourceErrorsMessage.TITLE_REQUIRED));
    }

    [Fact]
    public async Task Error_Expense_Not_Found()
    {
        var request = RequestExpensesJsonBuilder.Build();

        var result = await DoPut(requestUri: $"{Method}/1000", request: request, token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessage").EnumerateArray();

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(ResourceErrorsMessage.EXPENSES_NOT_FOUND));
    }


}
