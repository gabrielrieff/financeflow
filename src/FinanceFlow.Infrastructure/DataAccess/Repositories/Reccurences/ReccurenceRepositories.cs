using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories.Reccurences;
using FinanceFlow.Domain.Repositories.Recurrences;
using Microsoft.EntityFrameworkCore;

namespace FinanceFlow.Infrastructure.DataAccess.Repositories.Reccurences;

public class ReccurenceRepositories : IRecurrenceWhiteOnlyRepository, IRecurrenceReadOnlyRepository, IRecurrenceUpdateOnlyRepository
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
             .AsNoTracking()
             .Where(r => ids.Contains(r.AccountID))
             .OrderBy(r => r.Start_Date)
             .ToListAsync();
    }

    public async Task<List<Recurrence>> GetGetStartAtAndEndAtByID(DateOnly start_at, DateOnly end_at, List<long> ids)
    {
        var start = new DateTime(start_at.Year, start_at.Month, 1);

        var lastDayOfEndMonth = DateTime.DaysInMonth(end_at.Year, end_at.Month);
        var end = new DateTime(end_at.Year, end_at.Month, lastDayOfEndMonth);

        return await _dbContext.Recurrences
             .AsNoTracking()
             .Where(r => ids.Contains(r.AccountID))
             .OrderBy(r => r.Start_Date)
             .ToListAsync();
    }

    public async Task<Recurrence?> GetByIdAccount(long accountId)
    {
        return await _dbContext.Recurrences.FirstOrDefaultAsync(r => r.AccountID == accountId);
    }

    public void Update(Recurrence recurrence)
    {
        _dbContext.Recurrences.Update(recurrence);
    }

    public async Task<Recurrence?> GetRecurrenceById(long accountID)
    {
        return await _dbContext.Recurrences.AsNoTracking().FirstOrDefaultAsync(r => r.AccountID == accountID);
    }
}
