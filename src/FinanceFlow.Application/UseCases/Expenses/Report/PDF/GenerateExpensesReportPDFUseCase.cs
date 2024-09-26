using FinanceFlow.Application.UseCases.Expenses.Report.PDF.Colors;
using FinanceFlow.Application.UseCases.Expenses.Report.PDF.Fonts;
using FinanceFlow.Domain.Entities;
using FinanceFlow.Domain.Extensions;
using FinanceFlow.Domain.Reports;
using FinanceFlow.Domain.Repositories.Expenses;
using FinanceFlow.Domain.Services.LoggedUser;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;

namespace FinanceFlow.Application.UseCases.Expenses.Report;

public class GenerateExpensesReportPDFUseCase : IGenerateExpensesReportPDFUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private const int HEIGHT_ROW_EXPENSES_TABLE = 25;
    private readonly IExpensesReadOnlyRepository _repository;
    private readonly ILoggedUser _loggedUser;

    public GenerateExpensesReportPDFUseCase(
        IExpensesReadOnlyRepository repository,
        ILoggedUser loggedUser)
    {
        _repository = repository;
        _loggedUser = loggedUser;

        GlobalFontSettings.FontResolver = new ExpensesReportResolveFonts();
    }

    public async Task<byte[]> Excute(DateOnly month)
    {
        var loggedUser = await _loggedUser.Get();

        var expenses = await _repository.FilterByMonth(loggedUser, month);

        if(expenses.Count == 0)
        {
            return [];
        }

        var document = CreateDocument(month, loggedUser.Name);
        var page = CreatePage(document);

        CreateHeaderWithName(page, loggedUser.Name);

        var sumExpenses = expenses.Sum(expenses => expenses.Amount);

        CreateTotalSpentSection(page: page, month: month, sumExpenses: sumExpenses);

        foreach(var expense in expenses)
        {
            var table = CreateExpenseTable(page);

            var row = table.AddRow();
            row.Height = HEIGHT_ROW_EXPENSES_TABLE;

            AddExpensesTitle(row.Cells[0], expense.Title);

            AddHeaderForAmount(row.Cells[3]);

            row = table.AddRow();
            row.Height = HEIGHT_ROW_EXPENSES_TABLE;

            row.Cells[0].AddParagraph(expense.Create_at.ToString("D"));
            SetStyleBaseForExpensesInformation(row.Cells[0]);
            row.Cells[0].Format.LeftIndent = 20;

            row.Cells[1].AddParagraph(expense.Create_at.ToString("t"));
            SetStyleBaseForExpensesInformation(row.Cells[1]);

            row.Cells[2].AddParagraph(expense.PaymentType.PaymentTypeToString());
            SetStyleBaseForExpensesInformation(row.Cells[2]);

            AddAmountForExpense(row.Cells[3], expense.Amount);

            if(string.IsNullOrWhiteSpace(expense.Description) == false)
            {
                var descriptionRow = table.AddRow();
                descriptionRow.Height = HEIGHT_ROW_EXPENSES_TABLE;
                descriptionRow.Cells[0].AddParagraph(expense.Description);
                descriptionRow.Cells[0].Format.LeftIndent = 20;

                descriptionRow.Cells[0].Format.Font = new Font
                {
                    Name = FontHelpers.Inter_Regular,
                    Size = 10,
                    Color = ColorsHelpers.BLACK
                };
                descriptionRow.Cells[0].Shading.Color = ColorsHelpers.GREEN_LIGHT;
                descriptionRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;
                descriptionRow.Cells[0].MergeRight = 2;
                descriptionRow.Cells[0].Format.LeftIndent = 20;


                descriptionRow.Cells[0].MergeRight = 2;

                row.Cells[3].MergeDown = 1;
            }



            AddWhiteSpace(table);
        }

        return RenderDocument(document);
    }

    private Document CreateDocument(DateOnly month, string author)
    {
        var document = new Document();

        document.Info.Title = $"{ResourceReportGenerationMessage.EXPENSES_FOR} {month:Y}";
        document.Info.Author = author;

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelpers.Inter_Regular;

        return document;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();

        section.PageSetup.PageFormat = PageFormat.A4;

        section.PageSetup.RightMargin = 40;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;

        return section;
    }

    private void CreateHeaderWithName(Section page, string name)
    {
        var table = page.AddTable();

        table.AddColumn("300");

        var row = table.AddRow();

        row.Cells[0].AddParagraph($"Olá, {name}!");
        row.Cells[0].Format.Font = new Font
        {
            Name = FontHelpers.Inter_Black,
            Size = 16
        };
        row.Cells[0].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
    }

    private void CreateTotalSpentSection(Section page, DateOnly month, decimal sumExpenses) 
    {
        var paragraph = page.AddParagraph();

        paragraph.Format.SpaceBefore = "40";
        paragraph.Format.SpaceAfter = "40";

        var title = string.Format(ResourceReportGenerationMessage.TOTAL_SPENT_IN, month.ToString("Y"));
        paragraph.AddFormattedText(
            title,
            new Font
            {
                Name = FontHelpers.Inter_Regular,
                Size = 15
            });

        paragraph.AddLineBreak();

        paragraph.AddFormattedText(
            $"{CURRENCY_SYMBOL} {sumExpenses:f2}",
            new Font
            {
                Name = FontHelpers.Inter_Black,
                Size = 50
            });
    }

    private Table CreateExpenseTable(Section page)
    {  
        var table = page.AddTable();

        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;
        return table;
    }

    private void AddExpensesTitle(Cell cell, string title)
    {
        cell.AddParagraph(title);
        cell.Format.Font = new Font
        {
            Name = FontHelpers.Inter_Black,
            Size = 14,
            Color = ColorsHelpers.BLACK
        };
        cell.Shading.Color = ColorsHelpers.RED_LIGHT;
        cell.VerticalAlignment = VerticalAlignment.Center;
        cell.MergeRight = 2;
        cell.Format.LeftIndent = 20;
    }

    private void AddHeaderForAmount(Cell cell)
    {
        cell.AddParagraph(ResourceReportGenerationMessage.AMOUNT);
        cell.Format.Font = new Font
        {
            Name = FontHelpers.Inter_Black,
            Size = 14,
            Color = ColorsHelpers.WHITE
        };
        cell.Shading.Color = ColorsHelpers.RED_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;

    }

    private void SetStyleBaseForExpensesInformation(Cell cell)
    {
        cell.Format.Font = new Font
        {
            Name = FontHelpers.Inter_Regular,
            Size = 12,
            Color = ColorsHelpers.BLACK
        };
        cell.Shading.Color = ColorsHelpers.GREEN_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddAmountForExpense(Cell cell, decimal amount)
    {
        cell.AddParagraph($"{CURRENCY_SYMBOL} -{amount:f2}");
        cell.Format.Font = new Font
        {
            Name = FontHelpers.Inter_Regular,
            Size = 14,
            Color = ColorsHelpers.BLACK
        };
        cell.Shading.Color = ColorsHelpers.WHITE;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = 30;
        row.Borders.Visible = false;

    }

    private byte[] RenderDocument(Document document)
    {
        var renderer = new PdfDocumentRenderer
        {
            Document = document,
        };

        renderer.RenderDocument();

        using var file = new MemoryStream();
        renderer.PdfDocument.Save(file);

        return file.ToArray();
    }
}
