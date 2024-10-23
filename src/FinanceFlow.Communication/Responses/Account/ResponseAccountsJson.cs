using FinanceFlow.Communication.Enums;

namespace FinanceFlow.Communication.Responses.Account;

public class ResponseAccountsJson
{
    public long ID { get; set; }

    public string Title { get; set; } = string.Empty;

    public TypeAccount TypeAccount { get; set; }

    public ICollection<Tag> Tags { get; set; } = [];

    public bool Status { get; set; }

    public long UserID { get; set; }

    public DateTime Create_at { get; set; }

    public RecurrenceResponseJson? RecurrenceResponseJson { get; set; }
    public List<TransactionResponseJson>? TransactionResponseJson { get; set; }
}


public class RecurrenceResponseJson
{
    public long ID { get; set; }

    public decimal Amount { get; set; }

    public long AccountID { get; set; }

    public DateTime Start_Date { get; set; }

    public DateTime End_Date { get; set; }
}

public class TransactionResponseJson

{
    public long ID { get; set; }

    public decimal Amount { get; set; }

    public string? Description { get; set; }

    public long AccountID { get; set; }

    public TypeAccount TypeAccount { get; set; }

    public DateTime Create_at { get; set; }
}
