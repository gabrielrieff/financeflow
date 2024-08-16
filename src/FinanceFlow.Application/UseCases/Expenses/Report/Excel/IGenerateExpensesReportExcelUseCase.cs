namespace FinanceFlow.Application.UseCases.Expenses.Report;

public interface IGenerateExpensesReportExcelUseCase
{
    Task<byte []> Excute(DateOnly month);
}
