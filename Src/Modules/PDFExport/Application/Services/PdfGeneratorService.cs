using ColombianCoffee.Src.Modules.PDFExport.Application.Interfaces;
using ColombianCoffee.src.Modules.Varieties.Application.DTOs;

// IMPORTANTE: Estos 'using' statements son cruciales para que la API fluida funcione correctamente.
// Proporcionan los métodos de extensión necesarios (como 'Page', 'Header', 'Content', etc.).
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
                QuestPDF.Fluent.Document.Create(container =>
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4);
                        page.Margin(40);
                        page.PageColor(Colors.White);
                        
                        page.Header()
                            .Text("Ficha Técnica de Variedad de Café")
                            .FontSize(24).Bold().FontColor(Colors.Brown.Medium);

                        page.Content()
                            .PaddingVertical(20)
                            .Column(col =>
                            {
                                col.Spacing(10);
                                
                                // Información de identidad
                                col.Item().Text("Identidad y Taxonomía").FontSize(16).Bold().FontColor(Colors.Brown.Medium);
                                col.Item().Text($"Nombre: {varietyDetail.Name}").FontSize(14);
                                col.Item().Text($"Nombre científico: {varietyDetail.ScientificName}").FontSize(12).Italic();
                                col.Item().Text($"Historia: {varietyDetail.History ?? "No disponible"}").FontSize(12);
                                col.Item().Text($"Especie: {varietyDetail.SpeciesName}").FontSize(12);
                                col.Item().Text($"Grupo genético: {varietyDetail.GeneticGroupName}").FontSize(12);
                                col.Item().Text($"Linaje: {varietyDetail.LineageName}").FontSize(12);
                                
                                col.Item().Height(20); // Espacio entre secciones
                                
                                // Características agronómicas
                                col.Item().Text("Características Agronómicas").FontSize(16).Bold().FontColor(Colors.Brown.Medium);
                                col.Item().Text($"Porte (altura): {varietyDetail.PlantHeight}").FontSize(12);
                                col.Item().Text($"Tamaño de grano: {varietyDetail.BeanSize}").FontSize(12);
                                col.Item().Text($"Potencial de rendimiento: {varietyDetail.YieldPotential}").FontSize(12);
                                col.Item().Text($"Tiempo de cosecha: {varietyDetail.HarvestTime ?? "No disponible"}").FontSize(12);
                                col.Item().Text($"Tiempo de maduración: {varietyDetail.MaturationTime ?? "No disponible"}").FontSize(12);
                                col.Item().Text($"Requerimiento nutricional: {varietyDetail.NutritionalRequirement ?? "No disponible"}").FontSize(12);
                                
                                col.Item().Height(20); // Espacio entre secciones
                                
                                // Resistencias
                                col.Item().Text("Resistencias").FontSize(16).Bold().FontColor(Colors.Brown.Medium);
                                col.Item().Text($"Resistencia a roya: {varietyDetail.RustResistance}").FontSize(12);
                                col.Item().Text($"Resistencia a antracnosis: {varietyDetail.AnthracnoseResistance}").FontSize(12);
                                col.Item().Text($"Resistencia a nematodos: {varietyDetail.NematodesResistance}").FontSize(12);
                                
                                col.Item().Height(20); // Espacio entre secciones
                                
                                // Condiciones de cultivo
                                col.Item().Text("Condiciones de Cultivo").FontSize(16).Bold().FontColor(Colors.Brown.Medium);
                                col.Item().Text($"Altitud mínima: {varietyDetail.MinAltitude} msnm").FontSize(12);
                                col.Item().Text($"Altitud máxima: {varietyDetail.MaxAltitude} msnm").FontSize(12);
                                col.Item().Text($"Calidad de altitud: {varietyDetail.AltitudeQualityLabel}").FontSize(12);
                                col.Item().Text($"Unidad de altitud: {varietyDetail.AltitudeUnit ?? "No disponible"}").FontSize(12);
                                
                                if (varietyDetail.PlantingDensityValue.HasValue && !string.IsNullOrEmpty(varietyDetail.PlantingDensityUnit))
                                {
                                    col.Item().Text($"Densidad de siembra: {varietyDetail.PlantingDensityValue} {varietyDetail.PlantingDensityUnit}").FontSize(12);
                                }
                            });

                        page.Footer()
                            .AlignCenter()
                            .Text(x => 
                            { 
                                x.Span("Generado el ").FontSize(10);
                                x.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm")).FontSize(10).Bold();
                                x.Span(" - Colombian Coffee System").FontSize(10);
                            });
                    });
                })
                .GeneratePdf(outputPath);
            });
        }
    }
}
