using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace FinanceFlow.Infrastructure.DataAccess.Repositories.Users;

internal class UserRepositories : IUserReadOnlyRepository, IUserWhiteOnlyRepository, IUserUpdateOnlyRepository
{

    private readonly FinanceFlowDbContext _dbContext;

    public UserRepositories(FinanceFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }


    public async Task<bool> ExistActiveUserWithEmail(string email)
    {
        return await _dbContext.Users.AnyAsync(u => u.Email == email);
    }


    public async Task<User?> GetUserByEmail(string email)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(user => user.Email == email);
    }

    public void Update(User user)
    {
        _dbContext.Users.Update(user);
    }

    public async Task<User> GetById(long Id)
    {
        return await _dbContext.Users.FirstAsync(user => user.Id == Id);
    }
}
