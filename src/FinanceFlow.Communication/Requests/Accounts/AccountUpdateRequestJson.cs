using FinanceFlow.Communication.Enums;

namespace FinanceFlow.Communication.Requests.Accounts;

public class AccountUpdateRequestJson
{
    public string Title { get; set; } = string.Empty;

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public TypeAccount TypeAccount { get; set; }

    public ICollection<Tag> Tags { get; set; } = [];

    public DateTime Start_Date { get; set; }

    public DateTime End_Date { get; set; }

}
