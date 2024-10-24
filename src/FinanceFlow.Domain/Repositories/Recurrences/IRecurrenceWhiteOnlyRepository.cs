using FinanceFlow.Domain.Entities;

namespace FinanceFlow.Domain.Repositories.Reccurences;

public interface IRecurrenceWhiteOnlyRepository
{
    Task Add(Recurrence account);
    ///// <summary>
    ///// This function returns TRUE if the deletion was successful
    ///// </summary>
    ///// <param name="id"></param>
    ///// <returns></returns>
    //Task DeleteById(long id);
}
