using CoBudget.Application.UseCases.Expenses.Reports.Pdf.Fonts;

using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;

namespace CoBudget.Application.UseCases.Expenses.Reports.Pdf;

public class GenerateExpensesReportPdfUseCase: IGenerateExpensesReportPdfUseCase
{
    //private Document document;
    //private Section section;

    public GenerateExpensesReportPdfUseCase()
    {
        GlobalFontSettings.FontResolver = new ReportFontResolver(); // Dependency Injection Font Resolver
    }
    public byte[] GerarRelatorioComTabela()
    {
        var document = CriarDocumento("Relatório Comissão sobre vendas - Detalhado");
        var section = AdicionarSecao(document);

        AdicionarCabecalho(section);
        AdicionarTabelaDados(section);
        AdicionarRodape(section);

        return RenderDocument(document);
    }

    public Document CriarDocumento(string nomeArquivo)
    {
        var document = new Document();
        document.Info.Title = nomeArquivo;

        Style style = document.Styles["Normal"];
        style.Font.Name = FontHelper.GOTHAM_LIGHT;
        style.Font.Size = 10;
        style.ParagraphFormat.SpaceBefore = 0;
        style.ParagraphFormat.SpaceAfter = 4;

        style = document.Styles.AddStyle("TabelaDados", "Normal");
        style.Font.Name = FontHelper.GOTHAM_LIGHT;
        style.Font.Size = 6;
        style.ParagraphFormat.SpaceBefore = 0;
        style.ParagraphFormat.SpaceAfter = 0;

        return document;
    }

    public Section AdicionarSecao(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();
        section.PageSetup.PageFormat = PageFormat.A4;
        section.PageSetup.LeftMargin = Unit.FromCentimeter(0.5);
        section.PageSetup.RightMargin = Unit.FromCentimeter(0.5);
        section.PageSetup.TopMargin = Unit.FromCentimeter(1.0);
        section.PageSetup.BottomMargin = Unit.FromCentimeter(1.5);

        return section;
    }

    private void AdicionarCabecalho(Section section)
    {
        // Logo e informações da empresa (lado esquerdo)
        Table headerTable = section.AddTable();
        headerTable.Borders.Visible = false;
        headerTable.AddColumn(Unit.FromCentimeter(4.0)); // Logo 
        headerTable.AddColumn(Unit.FromCentimeter(5.5)); // Info empresa
        headerTable.AddColumn(Unit.FromCentimeter(9.9)); // Título relatório

        Row headerRow = headerTable.AddRow();
        headerRow.Height = Unit.FromCentimeter(2.5);

        // Célula com logo e informações da empresa
        Cell empresaCell = headerRow.Cells[0];
        empresaCell.VerticalAlignment = VerticalAlignment.Top;
        empresaCell.Format.Alignment = ParagraphAlignment.Center;

        // Célula com informações da empresa
        Cell empresaInfoCell = headerRow.Cells[1];
        empresaCell.VerticalAlignment = VerticalAlignment.Top;

        // Simulando logo (você pode adicionar uma imagem real aqui)
        Paragraph logoPara = empresaCell.AddParagraph();
        logoPara.AddText("LOGO");
        logoPara.Format.Font.Size = 16;
        logoPara.Format.Font.Bold = true;
        logoPara.Format.SpaceBefore = 0;
        logoPara.Format.SpaceAfter = 6;

        // Informações da empresa
        Paragraph empresaPara = empresaInfoCell.AddParagraph();
        var apelido = empresaPara.AddFormattedText("ISALU MULTIMARCAS LTDA\n", new Font 
        { 
            Name = FontHelper.GOTHAM_MEDIUM,
        });
        empresaPara.AddText("ISALU MULTIMARCAS\n");
        empresaPara.AddText("55.296.521/0001-20\n");
        empresaPara.AddText("Quadra ACSO 11 Rua SO 7\n");
        empresaPara.AddText("PALMAS");
        empresaPara.Format.Font.Size = 8;
        //empresaPara.Format.SpaceBefore = 10;

        // Célula com título do relatório
        Cell tituloCell = headerRow.Cells[2];
        tituloCell.VerticalAlignment = VerticalAlignment.Top;
        tituloCell.Format.Alignment = ParagraphAlignment.Left;

        Paragraph tituloPara = tituloCell.AddParagraph();
        tituloPara.AddFormattedText("Comissões de Atendentes - Detalhado", new Font
        {
            Name = FontHelper.GOTHAM_MEDIUM,
            Size = 12
        });
        tituloPara.Format.Font.Size = 12;
        tituloPara.Format.SpaceAfter = 25;

        // Data do relatório
        Paragraph dataPara = tituloCell.AddParagraph();
        dataPara.AddText("29/01/2025 16:24");
        dataPara.Format.Font.Size = 8;
        //dataPara.Format.SpaceAfter = 8;

        // Linha separadora
        section.AddParagraph().AddLineBreak();
    }

    private void AdicionarTabelaDados(Section section)
    {
        // Criar tabela principal
        Table table = section.AddTable();
        table.Style = "TabelaDados";
        table.Rows.LeftIndent = 0;

        // Definir colunas com larguras específicas
        table.AddColumn(Unit.FromCentimeter(3.5));  // Cliente
        table.AddColumn(Unit.FromCentimeter(1.0));  // Venda
        table.AddColumn(Unit.FromCentimeter(3.5));  // Produto
        table.AddColumn(Unit.FromCentimeter(1.2));  // Vlr. UN 
        table.AddColumn(Unit.FromCentimeter(1.2));  // Qtde
        table.AddColumn(Unit.FromCentimeter(1.5));  // Vlr. Total
        table.AddColumn(Unit.FromCentimeter(1.2));  // % Desc 
        table.AddColumn(Unit.FromCentimeter(1.5));  // Vlr. Desc.
        table.AddColumn(Unit.FromCentimeter(1.7));  // Vlr. C/Desc
        table.AddColumn(Unit.FromCentimeter(1.7));  // Vlr. Com. 
        table.AddColumn(Unit.FromCentimeter(2.0));  // Operação 

        // Cabeçalho da tabela
        Row headerRow = table.AddRow();
        headerRow.Height = Unit.FromCentimeter(0.5);
        //headerRow.HeadingFormat = true;
        headerRow.Format.Font.Size = 5;
        headerRow.Shading.Color = Colors.LightGray;

        var headers = new List<THead> 
        {
            new THead { thead = "Cliente", alignment = ParagraphAlignment.Left },
            new THead { thead = "Venda", alignment = ParagraphAlignment.Center },
            new THead { thead = "Produto", alignment = ParagraphAlignment.Left },
            new THead { thead = "Vlr. UN", alignment = ParagraphAlignment.Right },
            new THead { thead = "Qtde", alignment = ParagraphAlignment.Center },
            new THead { thead = "Vlr. Total", alignment = ParagraphAlignment.Right },
            new THead { thead = "% Desc", alignment = ParagraphAlignment.Right },
            new THead { thead = "Vlr. Desc.", alignment = ParagraphAlignment.Right },
            new THead { thead = "Vlr. C/Desc", alignment = ParagraphAlignment.Right },
            new THead { thead = "Vlr. Com.", alignment = ParagraphAlignment.Right },
            new THead { thead = "Operação" , alignment = ParagraphAlignment.Left }
        };

        for (int i = 0; i < headers.Count; i++)
        {
            Cell cell = headerRow.Cells[i];
            cell.AddParagraph(headers[i].thead);
            cell.Format.Font.Name = FontHelper.GOTHAM_MEDIUM;          
            cell.Format.Font.Size = 7;
            cell.Format.Alignment = headers[i].alignment;
            cell.VerticalAlignment = VerticalAlignment.Center;
        }

        AdicionarDadosExemplo(table);

        AdicionarLinhaTotais(table);
    }

    private void AdicionarDadosExemplo(Table table)
    {
        // Dados de exemplo baseados na sua imagem
        var dados = new[]
        {
                new { Cliente = "1 - CONSUMIDOR", Venda = "270", Produto = "1 - Lanterna recarregável 15 LED", VlrUN = "189,90", Qtde = "1,000", VlrTotal = "189,90", Desc = "0,00", VlrDesc = "0,00", VlrCDesc = "189,90", VlrCom = "0,00", Operacao = "1 - VENDA" },
                new { Cliente = "1 - CONSUMIDOR", Venda = "271", Produto = "1 - Lanterna recarregável 15 LED", VlrUN = "189,90", Qtde = "1,000", VlrTotal = "189,90", Desc = "0,00", VlrDesc = "0,00", VlrCDesc = "170,91", VlrCom = "0,00", Operacao = "1 - VENDA" },
                new { Cliente = "1 - CONSUMIDOR", Venda = "271", Produto = "1 - Lanterna recarregável 15 LED", VlrUN = "189,90", Qtde = "1,000", VlrTotal = "189,90", Desc = "0,00", VlrDesc = "0,00", VlrCDesc = "170,91", VlrCom = "0,00", Operacao = "1 - VENDA" },
                new { Cliente = "1 - CONSUMIDOR", Venda = "272", Produto = "1 - Lanterna recarregável 15 LED", VlrUN = "189,90", Qtde = "1,000", VlrTotal = "189,90", Desc = "0,00", VlrDesc = "0,00", VlrCDesc = "189,90", VlrCom = "0,00", Operacao = "1 - VENDA" },
                new { Cliente = "1 - CONSUMIDOR", Venda = "272", Produto = "1 - Lanterna recarregável 15 LED", VlrUN = "189,90", Qtde = "1,000", VlrTotal = "189,90", Desc = "0,00", VlrDesc = "0,00", VlrCDesc = "189,90", VlrCom = "0,00", Operacao = "1 - VENDA" }
        };

        foreach (var item in dados)
        {
            Row row = table.AddRow();
            row.Height = Unit.FromCentimeter(0.5);

            row.Cells[0].AddParagraph(item.Cliente).Format.Alignment = ParagraphAlignment.Left;
            row.Cells[1].AddParagraph(item.Venda).Format.Alignment = ParagraphAlignment.Center;
            row.Cells[2].AddParagraph(item.Produto).Format.Alignment = ParagraphAlignment.Left;
            row.Cells[3].AddParagraph(item.VlrUN).Format.Alignment = ParagraphAlignment.Right;
            row.Cells[4].AddParagraph(item.Qtde).Format.Alignment = ParagraphAlignment.Center;
            row.Cells[5].AddParagraph(item.VlrTotal).Format.Alignment = ParagraphAlignment.Right;
            row.Cells[6].AddParagraph(item.Desc).Format.Alignment = ParagraphAlignment.Right;
            row.Cells[7].AddParagraph(item.VlrDesc).Format.Alignment = ParagraphAlignment.Right;
            row.Cells[8].AddParagraph(item.VlrCDesc).Format.Alignment = ParagraphAlignment.Right;
            row.Cells[9].AddParagraph(item.VlrCom).Format.Alignment = ParagraphAlignment.Right;
            row.Cells[10].AddParagraph(item.Operacao).Format.Alignment = ParagraphAlignment.Left;
        }
    }

    private void AdicionarLinhaTotais(Table table)
    {
        Row totalRow = table.AddRow();
        totalRow.Format.Font.Name = FontHelper.GOTHAM_MEDIUM;
        totalRow.Borders.Top.Width = 0.5;
        totalRow.Height = Unit.FromCentimeter(0.5);

        // Total: 111 (nas primeiras colunas)
        Cell totalCell = totalRow.Cells[0];
        totalCell.MergeRight = 4; // Mesclar as primeiras 5 colunas
        totalCell.AddParagraph("Total: 111");
        totalCell.Format.Font.Bold = true;
        totalCell.VerticalAlignment = VerticalAlignment.Center;

        // Valores dos totais (baseado na sua imagem)
        totalRow.Cells[5].AddParagraph("18.065,79").Format.Font.Bold = true;
        totalRow.Cells[5].Format.Alignment = ParagraphAlignment.Right;
        totalRow.Cells[5].VerticalAlignment = VerticalAlignment.Center;

        totalRow.Cells[6].AddParagraph(""); // % Desc vazio

        totalRow.Cells[7].AddParagraph("70,28");
        totalRow.Cells[7].Format.Alignment = ParagraphAlignment.Right;
        totalRow.Cells[7].VerticalAlignment = VerticalAlignment.Center;

        totalRow.Cells[8].AddParagraph("17.995,51");
        totalRow.Cells[8].Format.Alignment = ParagraphAlignment.Right;
        totalRow.Cells[8].VerticalAlignment = VerticalAlignment.Center;

        totalRow.Cells[9].AddParagraph("1,89");
        totalRow.Cells[9].Format.Alignment = ParagraphAlignment.Right;
        totalRow.Cells[9].VerticalAlignment = VerticalAlignment.Center;

        totalRow.Cells[10].AddParagraph(""); // Operação vazia
    }

    private void AdicionarRodape(Section section)
    {
        // Adicionar espaço antes do rodapé
        section.AddParagraph().AddLineBreak();

        // Você pode adicionar informações adicionais aqui se necessário
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

public class THead
{
    public string thead = string.Empty;
    public ParagraphAlignment alignment;
}

