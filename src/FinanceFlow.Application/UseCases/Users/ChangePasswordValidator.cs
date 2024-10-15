using FinanceFlow.Communication.Requests.Users;
using FluentValidation;

namespace FinanceFlow.Application.UseCases.Users;

public class ChangePasswordValidator : AbstractValidator<RequestChangePasswordJson>
{
    public ChangePasswordValidator()
    {
        RuleFor(user => user.NewPassword).SetValidator(new PasswordValidator<RequestChangePasswordJson>());

    }
}
