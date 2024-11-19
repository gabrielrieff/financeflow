using Bogus;
using FinanceFlow.Communication.Enums;
using FinanceFlow.Communication.Requests.Accounts;

namespace commonTestUtilities.Requests.Account;

public class RequestAccountJsonBuilder
{
    public static AccountRequestJson Build()
    {
        var faker = new Faker();

        return new Faker<AccountRequestJson>()
            .RuleFor(r => r.Title, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(r => r.Start_Date, DateTime.Now)
            .RuleFor(r => r.End_Date, DateTime.Now)
            .RuleFor(r => r.TypeAccount, faker => faker.PickRandom<TypeAccount>())
            .RuleFor(r => r.Amount, faker => faker.Random.Decimal(min: 1, max: 1000))
            .RuleFor(r => r.Tags, faker => faker.Make(2, () => faker.PickRandom<Tag>()));

    }
}
