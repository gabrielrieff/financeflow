using FinanceFlow.Domain.Entities;

namespace FinanceFlow.Domain.Repositories.Users;
public interface IUserUpdateOnlyRepository
{
    void Update(User user);

    Task<User> GetById(long Id);
}
