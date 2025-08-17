using System;
using System.Collections.Generic;
using System.Linq;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ColombianCoffee.src.Modules.Varieties.Domain.Entities;

namespace ColombianCoffee.src.Modules.PDFExport.Application.Services
{
    public class CoffeeReportPdfDesign : IDocument
    {
        private readonly IEnumerable<Variety> _varieties;
        private readonly string _backgroundPath;
        private readonly string _logoPath;

        public CoffeeReportPdfDesign(IEnumerable<Variety> varieties)
        {
            _varieties = varieties;
            _backgroundPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\src\img\Background.png");
            _logoPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\src\img\BrEd.png");
            
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4.Landscape());
                page.Margin(0); // Margen cero para el fondo completo
                
                // Fondo de página
                page.Background().Image(_backgroundPath).FitArea();
                
                // Contenido
                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeVarietiesTable);
                page.Footer().Element(ComposeFooter);
            });
        }

        private void ComposeHeader(IContainer container)
        {
            container
                .PaddingTop(40)
                .PaddingBottom(20)
                .AlignCenter()
                .Column(column =>
                {
                    column.Item()
                        .Text("Reporte de Variedades de Café Colombiano")
                        .FontSize(24)
                        .Bold()
                        .FontColor("#8B4513");
                    
                    column.Item()
                        .Text($"Generado el {DateTime.Now:dd/MM/yyyy}")
                        .FontSize(14)
                        .FontColor(Colors.Grey.Darken1);
                });
        }

        private void ComposeVarietiesTable(IContainer container)
        {
            container
                .PaddingHorizontal(40)
                .Table(table =>
                {
                    // Definición de columnas (ajustadas para landscape)
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(60);  // ID
                        columns.RelativeColumn(2);   // Nombre
                        columns.RelativeColumn(2);   // Nombre Científico
                        columns.RelativeColumn();    // Altitud
                        columns.RelativeColumn();    // Resistencia Roya
                        columns.RelativeColumn();    // Rendimiento
                    });

                    // Encabezados de columna
                    table.Header(header =>
                    {
                        header.Cell().Element(HeaderCellStyle).Text("ID");
                        header.Cell().Element(HeaderCellStyle).Text("Nombre");
                        header.Cell().Element(HeaderCellStyle).Text("Nombre Científico");
                        header.Cell().Element(HeaderCellStyle).Text("Altitud (msnm)");
                        header.Cell().Element(HeaderCellStyle).Text("Resistencia Roya");
                        header.Cell().Element(HeaderCellStyle).Text("Rendimiento");
                        
                        static IContainer HeaderCellStyle(IContainer container)
                        {
                            return container
                                .Background("#8B4513")
                                .Border(1)
                                .BorderColor(Colors.Brown.Darken3)
                                .PaddingVertical(8)
                                .PaddingHorizontal(5)
                                .AlignCenter()
                                .Text(text =>
                                {
                                    text.Span().FontColor(Colors.White).FontSize(12).Bold();
                                });
                        }
                    });

                    // Datos de las variedades
                    foreach (var (variety, index) in _varieties.Select((v, i) => (v, i)))
                    {
                        var rowStyle = index % 2 == 0 ? "#F5F5DC" : "#FAF0E6"; // Beige claro alternado

                        table.Cell().Element(CellStyle).Text(variety.Id.ToString());
                        table.Cell().Element(CellStyle).Text(variety.Name);
                        table.Cell().Element(CellStyle).Text(variety.ScientificName);
                        table.Cell().Element(CellStyle).Text($"{variety.MinAltitude}-{variety.MaxAltitude}");
                        table.Cell().Element(CellStyle).Text(variety.RustResistance);
                        table.Cell().Element(CellStyle).Text(variety.YieldPotential);
                        
                        IContainer CellStyle(IContainer container)
                        {
                            return container
                                .Background(rowStyle)
                                .Border(1)
                                .BorderColor(Colors.Brown.Lighten2)
                                .PaddingVertical(5)
                                .PaddingHorizontal(5)
                                .Text(text =>
                                {
                                    text.Span().FontColor(Colors.Brown.Darken4).FontSize(11);
                                });
                        }
                    }
                });
        }

        private void ComposeFooter(IContainer container)
        {
            container
                .PaddingBottom(20)
                .Row(row =>
                {
                    row.ConstantItem(60)
                        .Image(_logoPath)
                        .FitWidth();
                    
                    row.RelativeItem()
                        .PaddingLeft(10)
                        .Column(column =>
                        {
                            column.Item()
                                .Text("Elaborado por: Breakline Education")
                                .FontSize(11)
                                .FontColor(Colors.Brown.Darken3);
                            
                            column.Item()
                                .Text($"Total variedades: {_varieties.Count()}")
                                .FontSize(11)
                                .Bold()
                                .FontColor(Colors.Brown.Darken3);
                        });
                });
        }
    }
}