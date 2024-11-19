using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories.Accounts;
using Moq;

namespace commonTestUtilities.Repositories.Accunts;

public class AccountsUpdateOnlyRepositoryBuilder
{

    private readonly Mock<IAccountUpdateOnlyRepository> _repository;
    public AccountsUpdateOnlyRepositoryBuilder()
    {
        _repository = new Mock<IAccountUpdateOnlyRepository>();
    }

    public AccountsUpdateOnlyRepositoryBuilder GetById(User user, Account account)
    {
        if (account is not null)
            _repository.Setup(repo => repo.GetById(user, account.ID)).ReturnsAsync(account);

        return this;
    }

    public IAccountUpdateOnlyRepository Build() => _repository.Object;

}
