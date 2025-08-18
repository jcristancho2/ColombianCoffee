using ColombianCoffee.Src.Modules.Varieties.Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ColombianCoffee.Src.Modules.PDFExport.Application.Services
{
    public class CoffeeReportPdfDesign : IDocument
    {
        private readonly IEnumerable<Variety> _varieties;

        public CoffeeReportPdfDesign(IEnumerable<Variety> varieties)
        {
            _varieties = varieties;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(30);
                page.Size(PageSizes.A4);

                page.Header().Text("Reporte de Variedades de Café")
                    .SemiBold().FontSize(18).FontColor(Colors.Brown.Medium);

                page.Content().Table(table =>
                {
                    // columnas
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(50);   // ID
                        columns.RelativeColumn(2);   // Nombre
                        columns.RelativeColumn(2);   // Altura
                        columns.RelativeColumn(2);   // Sabor
                        columns.RelativeColumn(2);   // Aroma
                    });

                    // fila de encabezado
                    table.Header(header =>
                    {
                        header.Cell().Element(HeaderCellStyle).Text("ID")
                            .FontColor(Colors.White).Bold();
                        header.Cell().Element(HeaderCellStyle).Text("Nombre")
                            .FontColor(Colors.White).Bold();
                        header.Cell().Element(HeaderCellStyle).Text("Altura")
                            .FontColor(Colors.White).Bold();
                        header.Cell().Element(HeaderCellStyle).Text("Sabor")
                            .FontColor(Colors.White).Bold();
                        header.Cell().Element(HeaderCellStyle).Text("Aroma")
                            .FontColor(Colors.White).Bold();
                    });

                    // filas de datos
                    foreach (var variety in _varieties)
                    {
                        table.Cell().Element(CellStyle).Text(variety.Id.ToString());
                        table.Cell().Element(CellStyle).Text(variety.Name);
                    }
                });

                page.Footer()
                    .AlignRight()
                    .Text(text =>
                    {
                        text.Span("Página ").FontSize(10);
                        text.CurrentPageNumber().FontSize(10);
                        text.Span(" de ").FontSize(10);
                        text.TotalPages().FontSize(10);
                    });
            });
        }

        // estilos de celdas
        static IContainer HeaderCellStyle(IContainer container)
        {
            return container
                .Background("#8B4513")
                .Border(1)
                .BorderColor(Colors.Brown.Darken3)
                .PaddingVertical(8)
                .PaddingHorizontal(5)
                .AlignCenter();
        }

        static IContainer CellStyle(IContainer container)
        {
            return container
                .BorderBottom(1)
                .BorderColor(Colors.Grey.Lighten2)
                .PaddingVertical(6)
                .PaddingHorizontal(5)
                .AlignCenter();
        }
    }
}
