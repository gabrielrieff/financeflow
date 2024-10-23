namespace FinanceFlow.Domain.Entities;

public class Recurrence
{
    public long ID { get; set; }

    public DateTime Start_Date { get; set; }

    public DateTime End_Date { get; set; }

    public DateTime Create_at { get; set; }

    public DateTime Update_at { get; set; }

    public long AccountID { get; set; }

    public Account Account { get; set; } = default!;

}
