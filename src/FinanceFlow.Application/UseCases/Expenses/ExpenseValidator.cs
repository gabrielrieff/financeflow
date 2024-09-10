using FinanceFlow.Communication.Requests;
using FinanceFlow.Exception.Resource;
using FluentValidation;

namespace FinanceFlow.Application.UseCases.Expenses;

public class ExpenseValidator : AbstractValidator<RequestExpenseJson>
{
    public ExpenseValidator()
    {
        RuleFor(expense => expense.Title).NotEmpty()
            .WithMessage(ResourceErrorsMessage.TITLE_REQUIRED);

        RuleFor(expense => expense.Amount).GreaterThan(0)
            .WithMessage(ResourceErrorsMessage.AMOUNT_MUST_BE_GREATER_THAN_ZERO);

        RuleFor(expense => expense.Create_at).LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage(ResourceErrorsMessage.EXPENSES_CANNOT_FOR_THE_FUTURE);

        RuleFor(expense => expense.PaymentType).IsInEnum()
            .WithMessage(ResourceErrorsMessage.PAYMENT_TYPE_INVALID);
    }
}
