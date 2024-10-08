using DocumentFormat.OpenXml;
using FluentAssertions;
using System.Net;
using System.Net.Mime;

namespace WebApi.Test.Expenses.Reports;

public class GenerateExpensesReportTest : FinanceFlowClassFixture
{
    private const string Method = "api/Report";

    private readonly string _token;
    private readonly DateTime _expenseDate;
    private readonly String mn;
    private readonly String yy;


    public GenerateExpensesReportTest(CustomWebApplicationFactory webApplicationFactory) : base(webApplicationFactory)
    {
        _expenseDate = webApplicationFactory._expenseIdentity.GetDate();
        _token = webApplicationFactory._userIdentity.GetToken();

        mn = _expenseDate.Month.ToString();
        yy = _expenseDate.Year.ToString();
    }

[Fact]
    public async Task Sucess_Pdf()
    {

        var result = await DoGet(requestUri: $"{Method}/pdf?month={mn}-{yy}", token: _token);
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        result.Content.Headers.ContentType.Should().NotBeNull();
        result.Content.Headers.ContentType!.MediaType.Should().Be(MediaTypeNames.Application.Pdf);
    }

    [Fact]
    public async Task Sucess_Excel()
    {
        var result = await DoGet(requestUri: $"{Method}/excel?month={mn}-{yy}", token: _token);
        result.StatusCode.Should().Be(HttpStatusCode.OK);

        result.Content.Headers.ContentType.Should().NotBeNull();
        result.Content.Headers.ContentType!.MediaType.Should().Be(MediaTypeNames.Application.Octet);
    }

}
