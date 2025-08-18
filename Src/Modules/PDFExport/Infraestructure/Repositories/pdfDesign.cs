using System.IO;
using System.Linq;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace GenerarPdf
{
    public static class Document
    {
        static Document()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public static void GenerateCoffeePdf(string outputPath)
        {
            var document = new MyCoffeeDocument();
            document.GeneratePdf(outputPath);
            Console.WriteLine($"PDF guardado en: {outputPath}");
        }
    }

    internal class MyCoffeeDocument : IDocument
    {
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;
        
        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                string backgroundPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\src\img\Background.png");
                
                page.Size(PageSizes.A4);
                page.Margin(40);
                
                page.Background().Image(backgroundPath, ImageScaling.FitArea);
                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeBody);
                page.Footer().Element(ComposeFooter);
            });
        }

        private void ComposeHeader(IContainer container)
        {
            string imgPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\src\img\coffee.webp");

            container.Row(row =>
            {
                row.RelativeItem()
                    .Column(column =>
                    {
                        // Nombre común
                        column.Item()
                            .Text("INIFAB 8")
                            .FontSize(21)
                            .Bold();
                        
                        // Nombre científico
                        column.Item()
                            .PaddingTop(2)
                            .Text("INIFAB 8")
                            .FontSize(16)
                            .Italic();
                        
                        // Descripción
                        column.Item()
                            .PaddingTop(2)
                            .Text("Tall plants with large numerous leaves and fruits; highest yielding clone for the conditions of the coast of Chiapas, Mexico.")
                            .FontSize(13)
                            .Light();
                    });

                row.ConstantItem(150)
                    .Width(100)
                    .Height(100)
                    .Border(1)
                    .BorderColor("#8B4513")
                    .Image(imgPath, ImageScaling.FitHeight);
            });
        }

        private void ComposeBody(IContainer container)
        {
            container.Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.ConstantColumn(155);
                    columns.ConstantColumn(355);
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("Agronomics").FontSize(16).Bold();
                    
                    static IContainer CellStyle(IContainer container)
                    {
                        return container
                            .Background("#10FFFFFF")
                            .PaddingBottom(10);
                    }
                });

                foreach (var i in Enumerable.Range(0, 7))
                {
                    table.Cell()
                        .Element(c => AlternatingCellStyle(c, i))
                        .Text("info ~ info ~")
                        .FontSize(13);
                    
                    table.Cell()
                        .Element(c => AlternatingCellStyle(c, i))
                        .Text("info ~ info ~")
                        .FontSize(13);
                }

                static IContainer AlternatingCellStyle(IContainer container, int index)
                {
                    var backgroundColor = index % 2 == 0 ? "#608B4513" : "#80FFFFFF";
                    return container
                        .Background(backgroundColor)
                        .PaddingVertical(3)
                        .PaddingHorizontal(10);
                }
            });
        }

        private void ComposeFooter(IContainer container)
        {
            string imgPath2 = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\src\img\BrEd.png");

            container.Row(row =>
            {
                row.ConstantItem(45)
                    .Image(imgPath2);

                row.RelativeItem()
                    .Padding(10)
                    .PaddingLeft(6)
                    .Column(column =>
                    {
                        // Editor's name
                        column.Item()
                            .Text("Elaborado por: Breakline Education")
                            .FontSize(12)
                            .Light();

                        // Date
                        column.Item()
                            .Text(DateTime.Now.ToString("dd/MM/yyyy"))
                            .FontSize(12)
                            .Light();
                    });
            });
        }
    }
}