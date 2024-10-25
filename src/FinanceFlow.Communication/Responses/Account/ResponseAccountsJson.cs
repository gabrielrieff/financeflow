using FinanceFlow.Communication.Enums;

namespace FinanceFlow.Communication.Responses.Account;

public class CollectionAccountsResponseJson
{
    public ICollection<AccountsJson> responseAccountsJsons { get; set; } = [];
}

public class AccountsJson
{
    public DateTime Month { get; set; }

    public ICollection<AccountJson> Accounts { get; set; } = [];
}

public class AccountJson
{
    public long ID { get; set; }

    public decimal Amount { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public TypeAccount TypeAccount { get; set; }

    public List<Tag> Tags { get; set; } = [];

    public DateTime Start_Date { get; set; }

    public DateTime End_Date { get; set; }

    public DateTime DateCurrent { get; set; }

    public int InstallmentsCurrent { get; set; }

    public int InstallmentsOverall
    {
        get
        {
            int diferencaAnos = End_Date.Year - Start_Date.Year;
            int diferencaMeses = (diferencaAnos * 12) + End_Date.Month - Start_Date.Month;

            // Se o dia da data final for menor que o dia da data inicial, subtrai um mês
            if (End_Date.Day < Start_Date.Day)
            {
                diferencaMeses--;
            }

            return diferencaMeses;
        }
    }
    public decimal InstallmentAmount
    {
        get
        {
            if (InstallmentsOverall == 0)
            {
                return Amount;
            }

            return Amount / InstallmentsOverall;
        }
    }
}
