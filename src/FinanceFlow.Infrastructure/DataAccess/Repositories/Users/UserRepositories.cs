using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace FinanceFlow.Infrastructure.DataAccess.Repositories.Users;

internal class UserRepositories : IUserReadOnlyRepository, IUserWhiteOnlyRepository
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
}
