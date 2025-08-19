using ColombianCoffee.Src.Modules.PDFExport.Application.Interfaces;
using ColombianCoffee.Src.Modules.Varieties.Application.DTOs;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace ColombianCoffee.Src.Modules.PDFExport.Application.Services
{
    public class PdfGeneratorService : IPdfGenerator
    {
        public PdfGeneratorService()
        {
            QuestPDF.Settings.License = LicenseType.Community;
        }

        public async Task GenerateCoffeeVarietyDetailPdf(VarietyDetailDto varietyDetail, string outputPath)
        {
            await Task.Run(() => 
            {
                Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(1.5f, Unit.Centimetre);
                        page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Calibri"));
                        
                        string backgroundPath = Path.Combine(Directory.GetCurrentDirectory(), "Src", "assets", "background.jpg");
                        page.Background().Image(backgroundPath).FitArea();

                        page.Header()
                            .Height(30);

                        page.Content()
                            .PaddingVertical(20)
                            .Column(col =>
                            {
                                col.Spacing(12);
                                col.Item().Height(8);

                                col.Item()
                                    .Row(row =>
                                    {
                                        row.RelativeItem(3)
                                            .Column(titleCol =>
                                            {
                                                titleCol.Item()
                                                    .Text(varietyDetail.Name)
                                                    .FontSize(32)
                                                    .Bold()
                                                    .FontColor(Color.FromHex("#3F2E24"))
                                                    .FontFamily("Calibri");
                                                titleCol.Item().Height(8);
                                                
                                                titleCol.Item()
                                                    .Text(varietyDetail.ScientificName)
                                                    .FontSize(14)
                                                    .Italic()
                                                    .FontColor(Color.FromHex("#3F2E24"))
                                                    .FontFamily("Calibri");
                                                
                                                titleCol.Item().Height(12);
                                                titleCol.Item()
                                                    .Text(string.IsNullOrWhiteSpace(varietyDetail.History) ? "No disponible" : varietyDetail.History)
                                                    .FontSize(12)
                                                    .FontColor(Color.FromHex("#3F2E24"))
                                                    .FontFamily("Calibri");
                                            });
                                        
                                        row.RelativeItem(1)
                                            .AlignCenter()
                                            .AlignMiddle()
                                            .Width(80)
                                            .Height(80)
                                            .Background(Color.FromHex("#5C3F2B"))
                                            .Element(container =>
                                            {
                                                if (!string.IsNullOrWhiteSpace(varietyDetail.ImageUrl))
                                                {
                                                    // Construir la ruta completa de la imagen
                                                    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), varietyDetail.ImageUrl);
                                                    if (File.Exists(imagePath))
                                                    {
                                                        container.Image(imagePath).FitArea();
                                                    }
                                                    else
                                                    {
                                                        // Si no existe la imagen, mostrar un texto indicativo
                                                        container.Text("Imagen\nNo disponible")
                                                            .FontSize(8)
                                                            .FontColor(Color.FromHex("#FFFFFF"))
                                                            .FontFamily("Calibri");
                                                    }
                                                }
                                                else
                                                {
                                                    // Si no hay URL de imagen, mostrar texto indicativo
                                                    container.Text("Sin\nImagen")
                                                        .FontSize(8)
                                                        .FontColor(Color.FromHex("#FFFFFF"))
                                                        .FontFamily("Calibri");
                                                }
                                            });
                                    });

                                col.Item().Height(25);

                                // Sección: Agronomics (como en la imagen de referencia)
                                col.Item()
                                    .Text("Agronomics")
                                    .FontSize(20)
                                    .Bold()
                                    .FontColor(Color.FromHex("#3F2E24"))
                                    .FontFamily("Calibri");
                                
                                col.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(2);
                                        columns.RelativeColumn(3);
                                    });
                                    
                                    AddTableRow(table, "Porte (altura)", varietyDetail.PlantHeight, 0);
                                    AddTableRow(table, "Tamaño de grano", varietyDetail.BeanSize, 1);
                                    AddTableRow(table, "Potencial de rendimiento", varietyDetail.YieldPotential, 0);
                                    AddTableRow(table, "Tiempo de cosecha", varietyDetail.HarvestTime ?? "No disponible", 1);
                                    AddTableRow(table, "Tiempo de maduración", varietyDetail.MaturationTime ?? "No disponible", 0);
                                    AddTableRow(table, "Requerimiento nutricional", varietyDetail.NutritionalRequirement ?? "No disponible", 1);
                                });
                                
                                col.Item().Height(20);
                                
                                // Sección: Background (como en la imagen de referencia)
                                col.Item()
                                    .Text("Background")
                                    .FontSize(20)
                                    .Bold()
                                    .FontColor(Color.FromHex("#3F2E24"))
                                    .FontFamily("Calibri");
                                
                                col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(3);
                                });
                                
                                AddTableRow(table, "Origen", varietyDetail.LineageName ?? "No disponible", 0);
                                AddTableRow(table, "Características", varietyDetail.SpeciesName ?? "No disponible", 1);
                            });
                                
                                col.Item().Height(20);
                                
                                // Sección: Condiciones de cultivo con tabla
                                col.Item()
                                    .Text("Condiciones de Cultivo")
                                    .FontSize(20)
                                    .Bold()
                                    .FontColor(Color.FromHex("#3F2E24"))
                                    .FontFamily("Calibri");
                                
                                col.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(2);
                                        columns.RelativeColumn(3);
                                    });
                                    
                                    AddTableRow(table, "Altitud mínima", $"{varietyDetail.MinAltitude} msnm", 0);
                                    AddTableRow(table, "Altitud máxima", $"{varietyDetail.MaxAltitude} msnm", 1);
                                    AddTableRow(table, "Calidad de altitud", varietyDetail.AltitudeQualityLabel, 0);
                                    
                                    if (varietyDetail.PlantingDensityValue.HasValue && !string.IsNullOrEmpty(varietyDetail.PlantingDensityUnit))
                                    {
                                        AddTableRow(table, "Densidad de siembra", $"{varietyDetail.PlantingDensityValue} {varietyDetail.PlantingDensityUnit}", 1);
                                    }
                                });
                            });

                        // Pie de página
                        page.Footer()
                            .Padding(8)
                            .Row(row =>
                            {
                                row.RelativeItem(1)
                                    .AlignLeft()
                                    .AlignMiddle()
                                    .Height(50)
                                    .Image(Path.Combine(Directory.GetCurrentDirectory(), "Src", "assets", "logo.png"))
                                    .FitArea();
                                
                                row.RelativeItem(2)
                                    .AlignLeft()
                                    .AlignMiddle()
                                    .Column(centerCol =>
                                    {
                                        centerCol.Item()
                                            .Text("Elaborado por: Colombian Coffee System")
                                            .FontSize(10)
                                            .FontColor(Color.FromHex("#3F2E24"));
                                        
                                        centerCol.Item()
                                            .Text(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))
                                            .FontSize(10)
                                            .Bold()
                                            .FontColor(Color.FromHex("#3F2E24"));
                                    });
                                
                                row.RelativeItem(1)
                                    .AlignRight()
                                    .AlignMiddle();
                            });
                    });
                })
                .GeneratePdf(outputPath);
            });
        }

        /// <summary>
        /// Método auxiliar para agregar filas a las tablas con colores alternados
        /// </summary>
        private void AddTableRow(TableDescriptor table, string label, string value, int rowIndex)
        {
            // Colores exactos de la imagen: filas alternadas entre blanco y marrón claro
            var backgroundColor = rowIndex % 2 == 0 ? Color.FromHex("#F8F5F0") : Color.FromHex("#8C735F");
            
            table.Cell()
                .Background(backgroundColor)
                .Border(1)
                .BorderColor(Color.FromHex("#3F2E24")) // Color marrón oscuro para bordes
                .Padding(8)
                .Text(label)
                .FontSize(12)
                .FontColor(Color.FromHex("#3F2E24")) // Color marrón oscuro para texto
                .Bold();
            
            table.Cell()
                .Background(backgroundColor)
                .Border(1)
                .BorderColor(Color.FromHex("#3F2E24")) // Color marrón oscuro para bordes
                .Padding(8)
                .Text(value)
                .FontSize(12)
                .FontColor(Color.FromHex("#3F2E24")); // Color marrón oscuro para texto
        }
    }
}
