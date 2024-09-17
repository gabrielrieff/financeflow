using FinanceFlow.Domain.Repositories;
using Moq;

namespace commonTestUtilities.Repositories;

public static class IUnitOfWorkBuilder
{
    public static IUnitOfWork Build()
    {
        var mock = new Mock<IUnitOfWork>();

        return mock.Object;
    }
}
