using FinanceFlow.Communication.Requests.Accounts;
using FinanceFlow.Exception.Resource;
using FluentValidation;

namespace FinanceFlow.Application.UseCases.Accounts;

public class AccountRegisterValidator : AbstractValidator<AccountRequestJson>
{
    public AccountRegisterValidator()
    {
        RuleFor(account => account.Amount).GreaterThan(0)
    .WithMessage("Deve ser fornecido o valor da conta.");
        RuleFor(account => account.Title).NotEmpty()
    .WithMessage("O titúlo não pode ser vazio.");
        RuleFor(account => account.TypeAccount).IsInEnum()
    .WithMessage("Tipo de conta deve ser fornecido.");
        RuleFor(account => account.Tags).ForEach(rule =>
        {
            rule.IsInEnum().WithMessage(ResourceErrorsMessage.TAG_TYPE_NOT_SUPPORTED);
        });

        RuleFor(account => account.Start_Date).LessThanOrEqualTo(x => x.End_Date)
.WithMessage("Data de inicio menor ou igual a data de fim.");
        RuleFor(account => account.End_Date).GreaterThanOrEqualTo(x => x.Start_Date)
    .WithMessage("Data de fim deve ser maior ou igual");
    }
}
