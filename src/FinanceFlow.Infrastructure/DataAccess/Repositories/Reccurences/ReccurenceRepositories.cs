using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories.Accounts;
using FinanceFlow.Domain.Repositories.Reccurences;
using FinanceFlow.Infrastructure.Migrations;
using Microsoft.EntityFrameworkCore;

namespace FinanceFlow.Infrastructure.DataAccess.Repositories.Reccurences;

public class ReccurenceRepositories : IReccurenceWhiteOnlyRepository, IReccurenceReadOnlyRepository
{
    private readonly FinanceFlowDbContext _dbContext;

    public ReccurenceRepositories(FinanceFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Recurrence reccurence)
    {
        await _dbContext.Recurrences.AddAsync(reccurence);
    }

    public async Task<List<Recurrence>> GetMonthByID(int month, int year, List<long> ids)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddDays(-1);

        return await _dbContext.Recurrences
             .Where(r => ids.Contains(r.AccountID) && r.Start_Date <= endDate && r.End_Date >= startDate)
             .OrderBy(r => r.Start_Date)
             .ToListAsync();
    }
}
