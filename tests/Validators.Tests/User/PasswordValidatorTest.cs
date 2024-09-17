using FinanceFlow.Application.UseCases.Users;
using FinanceFlow.Communication.Requests.Users;
using FluentAssertions;
using FluentValidation;

namespace Validators.Tests.User;

public class PasswordValidatorTest
{
    [Theory]
    [InlineData("")]
    [InlineData("        ")]
    [InlineData(null)]
    [InlineData("a")]
    [InlineData("aa")]
    [InlineData("aaa")]
    [InlineData("aaaa")]
    [InlineData("aaaaa")]
    [InlineData("aaaaaa")]
    [InlineData("aaaaaaa")]
    [InlineData("aaaaaaaa")]
    [InlineData("AAAAAAAA")]
    [InlineData("Aaaaaaaa")]
    [InlineData("Aaaaaaa1")]
    public void Error_Password_Empty(string password)
    {
        var validator = new PasswordValidator<RequestUserJson>();

        var result = validator
            .IsValid(new ValidationContext<RequestUserJson>(new RequestUserJson()), password);

        result.Should().BeFalse();
    }

}
