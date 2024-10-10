using Bogus;
using FinanceFlow.Communication.Requests.Users;

namespace commonTestUtilities.Requests.User;

public class RequestUpdateProfileJsonBuilder
{
    public static RequestUpdateProfileJson Build()
    {
        return new Faker<RequestUpdateProfileJson>()
            .RuleFor(user => user.Name, faker => faker.Person.FirstName)
            .RuleFor(user => user.Email, (faker, user) => faker.Internet.Email(user.Name));
    }
}
