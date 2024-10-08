using System.Net.Mime;
using FinanceFlow.Application.UseCases.Expenses.Report;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceFlow.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ReportController : ControllerBase
{
    [HttpGet("excel")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetExcel(
        [FromServices] IGenerateExpensesReportExcelUseCase useCase,
        [FromQuery] DateOnly month
    )
    {
        byte[] file = await useCase.Excute(month);

        if(file.Length > 0)
            return File(file, MediaTypeNames.Application.Octet, "Report.xlsx");

        return NoContent();
    }

    [HttpGet("pdf")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetPDF(
        [FromServices] IGenerateExpensesReportPDFUseCase useCase,
        [FromQuery] DateOnly month
    )
    {
        byte[] file = await useCase.Excute(month);

        if(file.Length > 0)
            return File(file, MediaTypeNames.Application.Pdf, "Report.pdf");

        return NoContent();
    }
}
