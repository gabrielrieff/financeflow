using commonTestUtilities.Requests.Expense;
using FinanceFlow.Application.UseCases.Expenses;
using FinanceFlow.Communication.Enums;
using FinanceFlow.Exception.Resource;
using FluentAssertions;

namespace Validators.Tests.Expenses;

public class ExpensesValidatorTests
{
    [Fact]
    public void Success()
    {
        var validator = new ExpenseValidator();
        var request = RequestExpensesJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ErrorTitleEmpty()
    {
        var validator = new ExpenseValidator();
        var request = RequestExpensesJsonBuilder.Build();
        request.Title = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorsMessage.TITLE_REQUIRED));
    }

    [Fact]
    public void ErrorCreate_AtFuture()
    {
        var validator = new ExpenseValidator();
        var request = RequestExpensesJsonBuilder.Build();
        request.Create_at = DateTime.UtcNow.AddDays(1);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorsMessage.EXPENSES_CANNOT_FOR_THE_FUTURE));
    }

    [Fact]
    public void ErrorPaymentsTypeInvalid()
    {
        var validator = new ExpenseValidator();
        var request = RequestExpensesJsonBuilder.Build();
        request.PaymentType = (PaymentType)700;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorsMessage.PAYMENT_TYPE_INVALID));
    }


    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    [InlineData(-10)]
    public void ErrorAmountInvalid(decimal amount)
    {
        var validator = new ExpenseValidator();
        var request = RequestExpensesJsonBuilder.Build();
        request.Amount = amount;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorsMessage.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
    }


    [Fact]
    public void Error_Tag_Invalid()
    {
        var validator = new ExpenseValidator();
        var request = RequestExpensesJsonBuilder.Build();
        request.Tags.Add((Tag)100);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorsMessage.TAG_TYPE_NOT_SUPPORTED));
    }


}
