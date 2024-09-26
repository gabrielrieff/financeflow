using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Repositories.Expenses;
using Microsoft.EntityFrameworkCore;

namespace FinanceFlow.Infrastructure.DataAccess.Repositories.Expenses;

internal class ExpensesRepositories : IExpensesReadOnlyRepository, IExpensesWhiteOnlyRepository, IExpensesUpdateOnlyRepository
{
    private readonly FinanceFlowDbContext _dbContext;

    public ExpensesRepositories(FinanceFlowDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Expense expense)
    {
        await _dbContext.Expenses.AddAsync(expense);
    }

    public async Task<List<Expense>> GetAll(User user)
    {
        return await _dbContext.Expenses.AsNoTracking().Where(expense => expense.UserId == user.Id).ToListAsync();
    }

    async Task<Expense?> IExpensesReadOnlyRepository.GetById(User user, long id)
    {
        return await _dbContext.Expenses
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id && x.UserId == user.Id);
    }

    async Task<Expense?> IExpensesUpdateOnlyRepository.GetById(User user, long id)
    {
        return await _dbContext.Expenses.FirstOrDefaultAsync(x => x.Id == id && x.UserId == user.Id);
    }

    public async Task DeleteById(long id)
    {
        var result = await _dbContext.Expenses.FirstAsync(e => e.Id == id);

        _dbContext.Expenses.Remove(result);
    }

    public void Update(Expense expense)
    {
        _dbContext.Expenses.Update(expense);
    }

    public async Task<List<Expense>> FilterByMonth(User user, DateOnly date)
    {
        var startDate = new DateTime(year: date.Year, month: date.Month, day: 1).Date;

        var daysInMonth = DateTime.DaysInMonth(year: date.Year, month: date.Month);
        var endDate = new DateTime(year: date.Year, month: date.Month, day: daysInMonth, hour: 23, minute: 59, second: 59);

        return await _dbContext.Expenses
        .AsNoTracking()
        .Where(ex => ex.UserId == user.Id && ex.Create_at >= startDate && ex.Create_at <= endDate)
        .OrderBy(ex => ex.Create_at)
        .ToListAsync();
    }
}
