using FinanceFlow.Domain.Entities;

namespace FinanceFlow.Domain.Repositories.Recurrences;

public interface IRecurrenceUpdateOnlyRepository
{
    Task<Recurrence?> GetByIdAccount(long accountId);
    void Update(Recurrence recurrence);
}
