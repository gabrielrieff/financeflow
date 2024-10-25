using FinanceFlow.Domain.Enums;

namespace FinanceFlow.Domain.Entities;

public class Account
{
    public long ID { get; set; }

    public decimal Amount { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public TypeAccount TypeAccount { get; set; }

    public ICollection<Tag> Tags { get; set; } = [];

    public bool Recurrence { get; set; }

    public int Installment {  get; set; }

    public DateTime Create_at { get; set; }

    public DateTime Update_at { get; set; }

    public long UserID { get; set; }

    public User User { get; set; } = default!;
}