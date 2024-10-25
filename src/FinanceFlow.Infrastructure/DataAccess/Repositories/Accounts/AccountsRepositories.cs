using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories.Accounts;
using Microsoft.EntityFrameworkCore;

namespace FinanceFlow.Infrastructure.DataAccess.Repositories.Accounts;

public class AccountRepositories : IAccountWhiteOnlyRepository, IAccountsReadOnlyRepository, IAccountUpdateOnlyRepository
{
    private readonly FinanceFlowDbContext _dbContext;

    public AccountRepositories(FinanceFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Account> Add(Account account)
    {
        var result = await _dbContext.Accounts.AddAsync(account);

        return result.Entity;
    }

    public async Task<List<Account>> GetMonth(int month, int year, long userId)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        return await GetFullAccount()
            .AsNoTracking()
            .Where(a => a.Create_at >= startDate && a.Create_at <= endDate && a.UserID == userId)
            .ToListAsync();
    }

    public async Task<Account?> GetById(User user, long id)
    {
        return await GetFullAccount()
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ID == id && x.UserID == user.Id);
    }
    
    public async Task DeleteById(long id)
    {
        var result = await _dbContext.Accounts.FirstAsync(x => x.ID == id);

        _dbContext.Accounts.Remove(result);
    }

    public async Task<List<Account>> GetStartAtAndEndAt(DateOnly start_at, DateOnly end_at, long userId)
    {
        var start = new DateTime(start_at.Year, start_at.Month, 1);

        var lastDayOfEndMonth = DateTime.DaysInMonth(end_at.Year, end_at.Month);
        var end = new DateTime(end_at.Year, end_at.Month, lastDayOfEndMonth);

        return await GetFullAccount()
            .AsNoTracking()
            .Where(a => a.Create_at >= start && a.Create_at <= end && a.UserID == userId)
            .OrderBy(a => a.Create_at)
            .ToListAsync();
    }

    public void Update(Account account)
    {
        _dbContext.Accounts.Update(account);
    }

    private Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Account, ICollection<Tag>> GetFullAccount()
    {
        return _dbContext.Accounts
           .Include(account => account.Tags);
    }

}
