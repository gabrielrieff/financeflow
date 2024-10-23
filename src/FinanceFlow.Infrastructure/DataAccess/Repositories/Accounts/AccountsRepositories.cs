using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories.Accounts;
using Microsoft.EntityFrameworkCore;

namespace FinanceFlow.Infrastructure.DataAccess.Repositories.Accounts;

public class AccountRepositories : IAccountWhiteOnlyRepository, IAccountsReadOnlyRepository
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
        var endDate = startDate.AddMonths(1).AddDays(-1); // Último dia do mês

        // Consulta as contas criadas no mês/ano
        return await _dbContext.Accounts
            .Where(a => a.Create_at >= startDate && a.Create_at <= endDate && a.Status == true && a.UserID == userId)
            .ToListAsync();
    }

    public async Task<Account?> GetById(User user, long id)
    {
        return await GetFullExpenses()
            .FirstOrDefaultAsync(x => x.ID == id && x.UserID == user.Id);
    }
    public async Task DeleteById(long id)
    {
        var result = await _dbContext.Accounts.FirstAsync(x => x.ID == id);

        _dbContext.Accounts.Remove(result);
    }

    private Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Account, ICollection<Tag>> GetFullExpenses()
    {
        return _dbContext.Accounts
           .Include(expense => expense.Tags);
    }

}
