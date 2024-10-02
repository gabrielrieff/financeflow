using FinanceFlow.Communication.Enums;
using FinanceFlow.Exception.Resource;
using FluentAssertions;
using System.Globalization;
using System.Net;
using System.Resources;
using System.Text.Json;

namespace WebApi.Test.Expenses.DeleteById;

public class DeleteByIdExpenseTest : FinanceFlowClassFixture
{
    private const string Method = "api/Expenses";

    private readonly string _token;
    private readonly long _expenseId;

    public DeleteByIdExpenseTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _token = webApplicationFactory._userIdentity.GetToken();
        _expenseId = webApplicationFactory._expenseIdentity.GetId();
    }

    [Fact]
    public async Task Success()
    {
        var result = await DoDelete(requestUri: $"{Method}/{_expenseId}", token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NoContent);

        result = await DoGet(requestUri: $"{Method}/{_expenseId}", token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task Error_Expense_Not_Fount()
    {
        var result = await DoDelete(requestUri: $"{Method}/1000", token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessage").EnumerateArray();

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(ResourceErrorsMessage.EXPENSES_NOT_FOUND));
    }
}
