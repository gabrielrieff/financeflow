using Bogus;
using FinanceFlow.Communication.Enums;
using FinanceFlow.Communication.Requests;

namespace commonTestUtilities.Requests.Expense;

public class RequestExpensesJsonBuilder
{
    public static RequestExpenseJson Build()
    {
        var faker = new Faker();

        return new Faker<RequestExpenseJson>()
            .RuleFor(r => r.Title, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(r => r.Create_at, faker => faker.Date.Past())
            .RuleFor(r => r.PaymentType, faker => faker.PickRandom<PaymentsType>())
            .RuleFor(r => r.Amount, faker => faker.Random.Decimal(min: 1, max: 1000));

    }
}
