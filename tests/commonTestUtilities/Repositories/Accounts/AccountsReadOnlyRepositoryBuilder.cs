using FinanceFlow.Domain.Repositories.Accounts;
using Moq;

namespace commonTestUtilities.Repositories.Accounts;

public class AccountsReadOnlyRepositoryBuilder
{
    private readonly Mock<IAccountsReadOnlyRepository> _repository;

    public AccountsReadOnlyRepositoryBuilder()
    {
        _repository = new Mock<IAccountsReadOnlyRepository>();
    }

    public IAccountsReadOnlyRepository Build() => _repository.Object;

}
