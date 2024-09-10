using FinanceFlow.Communication.Requests.Users;
using FinanceFlow.Exception.Resource;
using FluentValidation;

namespace FinanceFlow.Application.UseCases.Users;

public class RegisterUserValidator : AbstractValidator<RequestUserJson>
{

    public RegisterUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty()
            .WithMessage(ResourceErrorsMessage.NAME_REQUIRED);

        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorsMessage.EMAIL_REQUIRED)
            .EmailAddress()
            .WithMessage(ResourceErrorsMessage.EMAIL_INVALID);

        RuleFor(user => user.Password).SetValidator(new PasswordValidator<RequestUserJson>());


    }
}
