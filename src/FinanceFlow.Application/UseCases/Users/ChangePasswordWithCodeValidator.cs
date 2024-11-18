using FinanceFlow.Communication.Requests.Users;
using FluentValidation;

namespace FinanceFlow.Application.UseCases.Users;

public class ChangePasswordWithCodeValidator : AbstractValidator<RequestRecoverPasswordWithCode>
{
    public ChangePasswordWithCodeValidator()
    {
        RuleFor(user => user.NewPassword).SetValidator(new PasswordValidator<RequestRecoverPasswordWithCode>());

    }
}
