using commonTestUtilities.Requests.Account;
using commonTestUtilities.Requests.User;
using FinanceFlow.Application.UseCases.Accounts;
using FinanceFlow.Application.UseCases.Users;
using FinanceFlow.Communication.Enums;
using FinanceFlow.Exception.Resource;
using FluentAssertions;

namespace Validators.Tests.Account;

public class AccountRegisterValidatorTest
{

    [Fact]
    public void Success()
    {
        var validator = new AccountRegisterValidator();
        var request = RequestAccountJsonBuilder.Build();

        var result = validator.Validate(request);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-2)]
    [InlineData(-10)]
    public void ErrorAmountInvalid(decimal amount)
    {
        var validator = new AccountRegisterValidator();
        var request = RequestAccountJsonBuilder.Build();
        request.Amount = amount;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorsMessage.AMOUNT_MUST_BE_GREATER_THAN_ZERO));
    }

    [Fact]
    public void ErrorTitleEmpty()
    {
        var validator = new AccountRegisterValidator();
        var request = RequestAccountJsonBuilder.Build();
        request.Title = string.Empty;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorsMessage.TITLE_REQUIRED));
    }

    [Fact]
    public void ErrorStartBiggerEndAt()
    {
        var validator = new AccountRegisterValidator();
        var request = RequestAccountJsonBuilder.Build();
        request.Start_Date = DateTime.Now.AddDays(3);
        request.End_Date = DateTime.Now;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorsMessage.START_AT_FUTERE));
    }

    [Fact]
    public void ErrorTypeAccount()
    {
        var validator = new AccountRegisterValidator();
        var request = RequestAccountJsonBuilder.Build();
        request.TypeAccount = (TypeAccount)4;

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorsMessage.TYPE_ACCOUNT_REQUIRED));
    }

    [Fact]
    public void Error_Tag_Invalid()
    {
        var validator = new AccountRegisterValidator();
        var request = RequestAccountJsonBuilder.Build();
        request.Tags.Add((Tag)100);

        var result = validator.Validate(request);

        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorsMessage.TAG_TYPE_NOT_SUPPORTED));
    }

}
