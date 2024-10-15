using FinanceFlow.Communication.Enums;

namespace FinanceFlow.Communication.Responses.Expenses;

public class ResponseExpenseJson
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime Create_at { get; set; }

    public decimal Amount { get; set; }

    public PaymentType PaymentType { get; set; }

    public List<Tag> Tags { get; set; } = [];
}
