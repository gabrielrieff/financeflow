using FinanceFlow.Communication.Requests.Accounts;
using FinanceFlow.Exception.Resource;
using FluentValidation;

namespace FinanceFlow.Application.UseCases.Accounts;

public class AccountRegisterValidator : AbstractValidator<AccountRequestJson>
{
    public AccountRegisterValidator()
    {
        RuleFor(account => account.Amount).GreaterThan(0)
            .WithMessage(ResourceErrorsMessage.AMOUNT_MUST_BE_GREATER_THAN_ZERO);
        RuleFor(account => account.Title).NotEmpty()
            .WithMessage(ResourceErrorsMessage.TITLE_REQUIRED);
        RuleFor(account => account.TypeAccount).IsInEnum()
            .WithMessage(ResourceErrorsMessage.TYPE_ACCOUNT_REQUIRED);
        RuleFor(account => account.Tags).ForEach(rule =>
        {
            rule.IsInEnum().WithMessage(ResourceErrorsMessage.TAG_TYPE_NOT_SUPPORTED);
        });
        RuleFor(account => account.Start_Date).LessThanOrEqualTo(x => x.End_Date)
            .WithMessage(ResourceErrorsMessage.START_AT_FUTERE);
    }
}
