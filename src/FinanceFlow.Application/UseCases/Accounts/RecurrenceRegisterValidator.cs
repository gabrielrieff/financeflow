using FinanceFlow.Communication.Requests.Accounts;
using FluentValidation;

namespace FinanceFlow.Application.UseCases.Accounts;

public class ReccurenceRegisterValidator : AbstractValidator<RecurrenceRequestJson>
{
    public ReccurenceRegisterValidator()
    {
        RuleFor(recurrence => recurrence.Start_Date).LessThanOrEqualTo(x => x.End_Date)
    .WithMessage("Data de inicio menor ou igual a data de fim.");
        RuleFor(recurrence => recurrence.End_Date).GreaterThanOrEqualTo(x => x.Start_Date)
    .WithMessage("Data de fim deve ser maior ou igual");

    }
}
