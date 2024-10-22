namespace FinanceFlow.Domain.Entities;

public class Tag
{
    public long Id { get; set; }

    public Enums.Tag Value { get; set; }

    public long AccountId { get; set; }

    public Account Account { get; set; } = default!;
}
