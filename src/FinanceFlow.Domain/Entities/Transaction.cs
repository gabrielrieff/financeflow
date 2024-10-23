using FinanceFlow.Domain.Enums;

namespace FinanceFlow.Domain.Entities;

public class Transaction
{
    public long ID { get; set; }

    public decimal Amount { get; set; }

    public TypeAccount TypeAccount { get; set; }

    public DateTime Create_at { get; set; }

    public long AccountID { get; set; }

    public Account Account { get; set; } = default!;
}
