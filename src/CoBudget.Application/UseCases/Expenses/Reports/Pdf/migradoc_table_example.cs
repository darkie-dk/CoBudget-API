using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using System.Diagnostics;

public class MigraDocTableExample
{
    public void GerarRelatorioComTabela()
    {
        // Criar documento
        Document document = new Document();
        document.Info.Title = "Relatório com Tabela";
        document.Info.Subject = "Exemplo de estruturação de tabela";
        document.Info.Author = "Sistema";

        // Definir estilo
        DefineStyles(document);

        // Criar seção
        Section section = document.AddSection();
        section.PageSetup.TopMargin = "2cm";
        section.PageSetup.BottomMargin = "2cm";
        section.PageSetup.LeftMargin = "2cm";
        section.PageSetup.RightMargin = "2cm";

        // Adicionar título
        Paragraph title = section.AddParagraph("RELATÓRIO DE VENDAS");
        title.Format.Font.Name = "Arial";
        title.Format.Font.Size = 16;
        title.Format.Font.Bold = true;
        title.Format.SpaceAfter = "1cm";
        title.Format.Alignment = ParagraphAlignment.Center;

        // Criar tabela
        Table table = CriarTabela(section);

        // Adicionar dados à tabela
        PreencherTabela(table);

        // Renderizar documento
        PdfDocumentRenderer pdfRenderer = new PdfDocumentRenderer(true);
        pdfRenderer.Document = document;
        pdfRenderer.RenderDocument();

        // Salvar arquivo
        string filename = "RelatorioTabela.pdf";
        pdfRenderer.PdfDocument.Save(filename);
        Process.Start(filename);
    }

    private Table CriarTabela(Section section)
    {
        // Criar tabela
        Table table = section.AddTable();
        table.Style = "Table";
        table.Borders.Width = 0.25;
        table.Borders.Left.Width = 0.5;
        table.Borders.Right.Width = 0.5;
        table.Rows.LeftIndent = 0;

        // Definir colunas e suas larguras
        Column column1 = table.AddColumn("3cm");    // Código
        column1.Format.Alignment = ParagraphAlignment.Center;

        Column column2 = table.AddColumn("6cm");    // Produto
        column2.Format.Alignment = ParagraphAlignment.Left;

        Column column3 = table.AddColumn("2.5cm");  // Quantidade
        column3.Format.Alignment = ParagraphAlignment.Right;

        Column column4 = table.AddColumn("3cm");    // Preço Unit.
        column4.Format.Alignment = ParagraphAlignment.Right;

        Column column5 = table.AddColumn("3cm");    // Total
        column5.Format.Alignment = ParagraphAlignment.Right;

        // Criar linha de cabeçalho
        Row headerRow = table.AddRow();
        headerRow.HeadingFormat = true;
        headerRow.Format.Alignment = ParagraphAlignment.Center;
        headerRow.Format.Font.Bold = true;
        headerRow.Format.Font.Color = Colors.White;
        headerRow.Shading.Color = Colors.DarkBlue;
        headerRow.Height = "1cm";

        // Adicionar texto aos cabeçalhos
        headerRow.Cells[0].AddParagraph("Código");
        headerRow.Cells[0].Format.Alignment = ParagraphAlignment.Center;
        headerRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;

        headerRow.Cells[1].AddParagraph("Produto");
        headerRow.Cells[1].Format.Alignment = ParagraphAlignment.Center;
        headerRow.Cells[1].VerticalAlignment = VerticalAlignment.Center;

        headerRow.Cells[2].AddParagraph("Qtd");
        headerRow.Cells[2].Format.Alignment = ParagraphAlignment.Center;
        headerRow.Cells[2].VerticalAlignment = VerticalAlignment.Center;

        headerRow.Cells[3].AddParagraph("Preço Unit.");
        headerRow.Cells[3].Format.Alignment = ParagraphAlignment.Center;
        headerRow.Cells[3].VerticalAlignment = VerticalAlignment.Center;

        headerRow.Cells[4].AddParagraph("Total");
        headerRow.Cells[4].Format.Alignment = ParagraphAlignment.Center;
        headerRow.Cells[4].VerticalAlignment = VerticalAlignment.Center;

        return table;
    }

    private void PreencherTabela(Table table)
    {
        // Dados de exemplo
        var produtos = new[]
        {
            new { Codigo = "001", Nome = "Notebook Dell Inspiron", Quantidade = 2, PrecoUnit = 2500.00m },
            new { Codigo = "002", Nome = "Mouse Logitech MX Master", Quantidade = 5, PrecoUnit = 350.00m },
            new { Codigo = "003", Nome = "Teclado Mecânico Corsair", Quantidade = 3, PrecoUnit = 450.00m },
            new { Codigo = "004", Nome = "Monitor Samsung 27\"", Quantidade = 1, PrecoUnit = 1200.00m },
            new { Codigo = "005", Nome = "Webcam Logitech C920", Quantidade = 4, PrecoUnit = 280.00m }
        };

        decimal totalGeral = 0;

        foreach (var produto in produtos)
        {
            Row row = table.AddRow();
            row.Height = "0.8cm";

            // Alternar cor das linhas
            if (table.Rows.Count % 2 == 0)
            {
                row.Shading.Color = Colors.LightGray;
            }

            decimal total = produto.Quantidade * produto.PrecoUnit;
            totalGeral += total;

            // Preencher células
            row.Cells[0].AddParagraph(produto.Codigo);
            row.Cells[0].Format.Alignment = ParagraphAlignment.Center;
            row.Cells[0].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[1].AddParagraph(produto.Nome);
            row.Cells[1].Format.Alignment = ParagraphAlignment.Left;
            row.Cells[1].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[2].AddParagraph(produto.Quantidade.ToString());
            row.Cells[2].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[2].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[3].AddParagraph(produto.PrecoUnit.ToString("C2"));
            row.Cells[3].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[3].VerticalAlignment = VerticalAlignment.Center;

            row.Cells[4].AddParagraph(total.ToString("C2"));
            row.Cells[4].Format.Alignment = ParagraphAlignment.Right;
            row.Cells[4].VerticalAlignment = VerticalAlignment.Center;
        }

        // Linha de total
        Row totalRow = table.AddRow();
        totalRow.Format.Font.Bold = true;
        totalRow.Shading.Color = Colors.LightBlue;
        totalRow.Height = "1cm";

        // Mesclar células para o total
        totalRow.Cells[0].MergeRight = 3;
        totalRow.Cells[0].AddParagraph("TOTAL GERAL");
        totalRow.Cells[0].Format.Alignment = ParagraphAlignment.Right;
        totalRow.Cells[0].VerticalAlignment = VerticalAlignment.Center;

        totalRow.Cells[4].AddParagraph(totalGeral.ToString("C2"));
        totalRow.Cells[4].Format.Alignment = ParagraphAlignment.Right;
        totalRow.Cells[4].VerticalAlignment = VerticalAlignment.Center;
        totalRow.Cells[4].Format.Font.Bold = true;
    }

    private void DefineStyles(Document document)
    {
        // Definir estilo da tabela
        Style style = document.Styles["Table"];
        style.Font.Name = "Arial";
        style.Font.Size = 9;

        // Estilo para cabeçalho
        style = document.Styles.AddStyle("TableHeader", "Table");
        style.Font.Bold = true;
        style.Font.Color = Colors.White;
        style.ParagraphFormat.Alignment = ParagraphAlignment.Center;
    }
}

// Para usar:
// var exemplo = new MigraDocTableExample();
// exemplo.GerarRelatorioComTabela();