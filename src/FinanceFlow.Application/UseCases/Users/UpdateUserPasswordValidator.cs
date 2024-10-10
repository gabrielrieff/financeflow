using FinanceFlow.Communication.Requests.Users;
using FluentValidation;

namespace FinanceFlow.Application.UseCases.Users;

public class UpdateUserPasswordValidator : AbstractValidator<RequestChangePasswordJson>
{
    public UpdateUserPasswordValidator()
    {
        RuleFor(user => user.NewPassword).SetValidator(new PasswordValidator<RequestChangePasswordJson>());

    }
}
