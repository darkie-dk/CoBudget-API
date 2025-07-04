using System.Reflection;
using CoBudget.Application.UseCases.Expenses.Reports.Pdf.Fonts;
using CoBudget.Domain;
using CoBudget.Domain.Entities;
using CoBudget.Domain.Repositories.Expenses;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CoBudget.Application.UseCases.Expenses.Reports.Pdf;

public class GenerateExpensesReportPdfUseCase: IGenerateExpensesReportPdfUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private readonly IExpensesReadRepository _expensesReadRepository;

    public GenerateExpensesReportPdfUseCase(IExpensesReadRepository repository)
    {
        _expensesReadRepository = repository;

        GlobalFontSettings.FontResolver = new ExpensesReportFontResolver();
    }

    public async Task<byte[]> Execute(DateOnly date)
    {
        var expenses = await _expensesReadRepository.FilterByMonth(date) ?? [];

        var document = CreateDocument(date);
        var page = CreateSection(document);

        CreateHeaderWithProfilePicAndName(page);

        var totalAmountExpenses = expenses.Sum(expenses => expenses.Amount);
        CreateTotalSpentParagraph(page, date, totalAmountExpenses);

        return RenderDocument(document);
    }

    private static Document CreateDocument(DateOnly date)
    {
        var document = new Document();
        document.Info.Title = $"{ResourceReportMessages.EXPENSES_OF} {date:Y}";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.RALEWAY_REGULAR;
        

        return document;
    }

    private static Section CreateSection(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = 40;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.BottomMargin = 80;

        return section;
    }

    private static void CreateHeaderWithProfilePicAndName(Section page)
    {
        var table = page.AddTable();
        table.AddColumn();
        table.AddColumn("300");

        var row = table.AddRow();

        var assembly = Assembly.GetExecutingAssembly();
        var directoryName = Path.GetDirectoryName(assembly.Location);
        var pathFile = Path.Combine(directoryName!, "Logo", "logo.png");

        row.Cells[0].AddImage(pathFile);
        row.Cells[1].AddParagraph("Hey, bro!");
        row.Cells[1].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 };
        row.Cells[1].VerticalAlignment = MigraDoc.DocumentObjectModel.Tables.VerticalAlignment.Center;
    }

    private static void CreateTotalSpentParagraph(Section page, DateOnly date, decimal totalAmountExpenses)
    {
        var paragraph = page.AddParagraph();
        paragraph.Format.SpaceBefore = "40";
        paragraph.Format.SpaceAfter = "40";

        var title = string.Format(ResourceReportMessages.TOTAL_SPENT_IN, date.ToString("Y"));

        paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });
        paragraph.AddLineBreak();

        paragraph.AddFormattedText($"{totalAmountExpenses} {CURRENCY_SYMBOL}", new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50 });
    }

    private static byte[] RenderDocument(Document document)
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
