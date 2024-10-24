using FinanceFlow.Domain.Entities;

namespace FinanceFlow.Domain.Repositories.Reccurences;
public interface IReccurenceReadOnlyRepository
{
    Task<List<Recurrence>> GetMonthByID(int year, int month, List<long> ids);
    Task<List<Recurrence>> GetGetStartAtAndEndAtByID(DateOnly start_at, DateOnly end_at, List<long> ids);
}
