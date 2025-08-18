using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using ColombianCoffee.Modules.Varieties.Domain.Entities;

namespace ColombianCoffee.src.Modules.PDFExport.Domain.Designs
{
    public class CoffeePdfDesign : IDocument
    {
        private readonly Variety _variety;

        public CoffeePdfDesign(Variety variety)
        {
            _variety = variety;
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                // Configuración de página
                page.Size(PageSizes.A4);
                page.Margin(0); // Eliminamos márgenes para el fondo completo
                
                // Fondo de página
                var backgroundPath = GetImagePath("Background.png");
                page.Background().Image(backgroundPath).FitArea();
                
                // Contenido estructurado
                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeBody);
                page.Footer().Element(ComposeFooter);
            });
        }

        private void ComposeHeader(IContainer container)
        {
            container
                .PaddingLeft(40)
                .PaddingRight(40)
                .PaddingTop(40)
                .PaddingBottom(30)
                .Row(row =>
                {
                    row.RelativeItem().Column(column =>
                    {
                        // Nombre común (usando datos reales de la variedad)
                        column.Item()
                            .AlignLeft()
                            .Text(_variety.Name)
                            .FontSize(21)
                            .Bold();
                        
                        // Nombre científico
                        column.Item()
                            .AlignLeft()
                            .PaddingTop(2)
                            .Text(_variety.ScientificName)
                            .FontSize(16)
                            .Italic();
                        
                        // Descripción
                        column.Item()
                            .AlignLeft()
                            .PaddingTop(2)
                            .Text(_variety.History)
                            .FontSize(13)
                            .Light();
                    });
                    
                    // Imagen del café (si está disponible)
                    if (!string.IsNullOrEmpty(_variety.ImageUrl))
                    {
                        row.ConstantItem(150)
                            .AlignRight()
                            .Width(100)
                            .Height(100)
                            .Border(1)
                            .BorderColor("#8B4513")
                            .Image(_variety.ImageUrl)
                            .FitHeight();
                    }
                });
        }

        private void ComposeBody(IContainer container)
        {
            container
                .PaddingLeft(40)
                .PaddingRight(40)
                .Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(155);
                        columns.ConstantColumn(355);
                    });

                    // Encabezado de la tabla
                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("Características Agronómicas").FontSize(16).Bold();
                        
                        static IContainer CellStyle(IContainer container)
                        {
                            return container
                                .Background("#10FFFFFF")
                                .PaddingBottom(10);
                        }
                    });

                    // Datos dinámicos de la variedad
                    AddVarietyDetailRow(table, 0, "Altitud", $"{_variety.MinAltitude}-{_variety.MaxAltitude} {_variety.AltitudeUnit?.Name}");
                    AddVarietyDetailRow(table, 1, "Resistencia a Roya", _variety.RustResistance);
                    AddVarietyDetailRow(table, 2, "Resistencia a Antracnosis", _variety.AnthracnoseResistance);
                    AddVarietyDetailRow(table, 3, "Potencial de Rendimiento", _variety.YieldPotential);
                    AddVarietyDetailRow(table, 4, "Densidad de Siembra", $"{_variety.PlantingDensityValue} {_variety.PlantingDensityUnit?.Name}");
                });
        }

        private void AddVarietyDetailRow(TableDescriptor table, int rowIndex, string label, string value)
        {
            table.Cell()
                .Element(c => CellStyle(c, rowIndex))
                .Text(label)
                .FontSize(13);
            
            table.Cell()
                .Element(c => CellStyle(c, rowIndex))
                .Text(value)
                .FontSize(13);
        }

        private static IContainer CellStyle(IContainer container, int rowIndex)
        {
            var backgroundColor = rowIndex % 2 == 0 ? "#608B4513" : "#80FFFFFF";
            
            return container
                .Background(backgroundColor)
                .PaddingVertical(3)
                .PaddingHorizontal(10);
        }

        private void ComposeFooter(IContainer container)
        {
            container
                .Padding(40)
                .Row(row =>
                {
                    var logoPath = GetImagePath("BrEd.png");
                    
                    row.ConstantItem(45)
                        .AlignLeft()
                        .PaddingTop(5)
                        .Image(logoPath);

                    row.RelativeItem()
                        .Padding(10)
                        .PaddingLeft(6)
                        .Column(column =>
                        {
                            // Editor
                            column.Item()
                                .AlignLeft()
                                .Text("Elaborado por: Breakline Education")
                                .FontSize(12)
                                .Light();

                            // Fecha
                            column.Item()
                                .AlignLeft()
                                .Text(DateTime.Now.ToString("dd/MM/yyyy HH:mm"))
                                .FontSize(12)
                                .Light();
                        });
                });
        }

        private string GetImagePath(string imageName)
        {
            return Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\src\img", imageName);
        }
    }
}