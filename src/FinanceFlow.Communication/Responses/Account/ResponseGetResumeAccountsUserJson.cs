namespace FinanceFlow.Communication.Responses.Account;

public class ResponseGetResumeAccountsUserJson
{
    public decimal Expenses { get; set; }

    public decimal Revenues { get; set; }

    public decimal Balance { 
        get {
            return Revenues - Expenses;
            } 
    }
}
