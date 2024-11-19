using Bogus;
using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Enums;

namespace commonTestUtilities.Entities;

public class AccountBuilder
{
    public static List<Account> Collection(User user, uint count = 2)
    {
        var list = new List<Account>();

        if (count == 0)
            count = 1;

        var accountID = 1;

        for (int i = 0; i < count; i++)
        {
            var account = Build(user);
            account.ID = accountID++;

            list.Add(account);
        }

        return list;

    }

    public static Account Build(User user)
    {
        return new Faker<Account>()
            .RuleFor(u => u.ID, _ => 1)
            .RuleFor(r => r.Amount, faker => faker.Random.Decimal(min: 1, max: 1000))
            .RuleFor(u => u.Title, faker => faker.Commerce.ProductName())
            .RuleFor(r => r.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(r => r.TypeAccount, faker => faker.PickRandom<TypeAccount>())
            .RuleFor(r => r.Tags, faker => faker.Make(1, () => new FinanceFlow.Domain.Entities.Tag
            {
                Id = 1,
                Value = faker.PickRandom<FinanceFlow.Domain.Enums.Tag>(),
                AccountId = 1,
            }))            
            .RuleFor(r => r.Recurrence, false)
            .RuleFor(r => r.Installment, 1)
            .RuleFor(r => r.Create_at, DateTime.Now)
            .RuleFor(r => r.Update_at, DateTime.Now)
            .RuleFor(r => r.Start_Date, DateTime.Now)
            .RuleFor(r => r.End_Date, DateTime.Now)
            .RuleFor(r => r.UserID, _ => user.Id);

    }
}
