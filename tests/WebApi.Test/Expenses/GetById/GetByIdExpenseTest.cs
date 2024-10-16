using FinanceFlow.Communication.Enums;
using FinanceFlow.Exception.Resource;
using FluentAssertions;
using System.Net;
using System.Text.Json;

namespace WebApi.Test.Expenses.GetById;

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
        var result = await DoGet(requestUri: $"{Method}/{_expenseId}", token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.OK);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        response.RootElement.GetProperty("id").GetInt64().Should().Be(_expenseId);
        response.RootElement.GetProperty("title").GetString().Should().NotBeNullOrWhiteSpace();
        response.RootElement.GetProperty("description").GetString().Should().NotBeNullOrWhiteSpace();
        response.RootElement.GetProperty("amount").GetDecimal().Should().BeGreaterThan(0);
        response.RootElement.GetProperty("tags").EnumerateArray().Should().NotBeNullOrEmpty();

        var paymentType = response.RootElement.GetProperty("paymentType").GetInt32();
        Enum.IsDefined(typeof(PaymentType), paymentType).Should().BeTrue();
    }

    [Fact]
    public async Task Error_Expense_Not_Fount()
    {
        var result = await DoGet(requestUri: $"{Method}/1000", token: _token);

        result.StatusCode.Should().Be(HttpStatusCode.NotFound);

        var body = await result.Content.ReadAsStreamAsync();

        var response = await JsonDocument.ParseAsync(body);

        var errors = response.RootElement.GetProperty("errorMessage").EnumerateArray();

        errors.Should().HaveCount(1).And.Contain(error => error.GetString()!.Equals(ResourceErrorsMessage.EXPENSES_NOT_FOUND));
    }
}
