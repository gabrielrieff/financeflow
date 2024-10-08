using FinanceFlow.Communication.Requests.Users;
using FinanceFlow.Exception.Resource;
using FluentValidation;

namespace FinanceFlow.Application.UseCases.Users;

public class UpdateUserValidator : AbstractValidator<RequestUpdateProfileJson>
{
    public UpdateUserValidator()
    {
        RuleFor(user => user.Name).NotEmpty()
    .WithMessage(ResourceErrorsMessage.NAME_REQUIRED);

        RuleFor(user => user.Email)
            .NotEmpty()
            .WithMessage(ResourceErrorsMessage.EMAIL_REQUIRED)
            .EmailAddress()
            .When(user => string.IsNullOrWhiteSpace(user.Email) == false, ApplyConditionTo.CurrentValidator)
            .WithMessage(ResourceErrorsMessage.EMAIL_INVALID);

    }
}
