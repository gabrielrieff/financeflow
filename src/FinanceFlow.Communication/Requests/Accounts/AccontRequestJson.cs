using FinanceFlow.Communication.Enums;

namespace FinanceFlow.Communication.Requests.Accounts;

public class AccountRequestJson
{

    public string Title { get; set; } = string.Empty;

    public TypeAccount TypeAccount { get; set; }

    public ICollection<Tag> Tags { get; set; } = [];

    public bool Status { get; set; }

    public long UserID { get; set; }

    public decimal Amount { get; set; }

    public DateTime Create_at { get; set; }

    public DateTime Update_at { get; set; }

    public RecurrenceRequestJson? RecurrenceRequestJson { get; set; }
    public TransactionRequestJson? TransactionRequestJson { get; set; }
}


public class RecurrenceRequestJson
{
    public DateTime Start_Date { get; set; }

    public DateTime End_Date { get; set; }
}

public class TransactionRequestJson
{
    public string? Description { get; set; }
}
