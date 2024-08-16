using PdfSharp.Fonts;
using System.Reflection;

namespace FinanceFlow.Application.UseCases.Expenses.Report.PDF.Fonts;

public class ExpensesReportResolveFonts : IFontResolver
{
    public byte[]? GetFont(string faceName)
    {
        var stream = ReadFontFile(faceName) ?? ReadFontFile(FontHelpers.Default_Font);
        var length = (int)stream!.Length;

        var data = new byte[length];

        stream.Read(buffer: data, offset: 0, count: length);

        return data;
    }

    public FontResolverInfo? ResolveTypeface(string familyName, bool bold, bool italic)
    {
        return new FontResolverInfo(familyName);
    }

    private Stream? ReadFontFile(string faceName)
    {
        var assemble = Assembly.GetExecutingAssembly();

        return assemble.GetManifestResourceStream($"FinanceFlow.Application.UseCases.Expenses.Report.PDF.Fonts.{faceName}.ttf");
    }
}
