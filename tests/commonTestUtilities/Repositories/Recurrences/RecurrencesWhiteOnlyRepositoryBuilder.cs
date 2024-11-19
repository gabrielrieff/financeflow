using FinanceFlow.Domain.Repositories.Reccurences;
using Moq;

namespace commonTestUtilities.Repositories.Recurrences;

public class RecurrencesWhiteOnlyRepositoryBuilder
{
    public static IRecurrenceWhiteOnlyRepository Build()
    {
        var mock = new Mock<IRecurrenceWhiteOnlyRepository>();

        return mock.Object;
    }
}
