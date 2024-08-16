namespace FinanceFlow.Application.UseCases.Expenses.Report;

public interface IGenerateExpensesReportPDFUseCase
{
    Task<byte []> Excute(DateOnly month);
}
