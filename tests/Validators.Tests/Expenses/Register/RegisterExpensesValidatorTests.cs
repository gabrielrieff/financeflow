using commonTestUtilities.Requests;
using FinanceFlow.Application.UseCases.Expenses;
using FinanceFlow.Application.UseCases.Expenses.Register;
using FinanceFlow.Communication.Enums;
using FinanceFlow.Exception;
using FluentAssertions;

namespace Validators.Tests.Expenses.Register;

public class RegisterExpensesValidatorTests
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
        request.PaymentType = (PaymentsType)700;

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

}
