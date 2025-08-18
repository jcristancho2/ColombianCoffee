using ColombianCoffee.Src.Modules.PDFExport.Application.Interfaces;
using ColombianCoffee.Src.Modules.Varieties.Application.DTOs;

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
                // Inicia la creación del documento PDF utilizando la API fluida de QuestPDF.
                // 'container' es el punto de entrada para definir el layout del documento.
                Document.Create(container =>
                {
                    // Define una página dentro del documento.
                    // 'page' es el punto de entrada para definir el contenido de una página individual.
                    container.Page(page =>
                    {
                        // Configura las propiedades básicas de la página: tamaño similar a la imagen de referencia
                        page.Size(PageSizes.A4); // Mantenemos A4 pero ajustamos márgenes
                        page.Margin(1.5f, Unit.Centimetre); // Márgenes más pequeños para aprovechar espacio
                        page.DefaultTextStyle(x => x.FontSize(12).FontFamily("Calibri"));
                        
                        // Configura la imagen de fondo usando el método correcto
                        string backgroundPath = Path.Combine(Directory.GetCurrentDirectory(), "Src", "assets", "background.jpg");
                        page.Background().Image(backgroundPath).FitArea();

                        // NOTA IMPORTANTE:
                        // Si el encabezado ("Ficha Técnica...") NO debe repetirse en cada página,
                        // lo movemos de 'page.Header()' y lo colocamos como el primer elemento en 'page.Content()'.
                        // 'page.Header()' ahora puede quedar vacío o contener elementos que SÍ deben repetirse,
                        // como un logo corporativo o un pie de página que se repita arriba.
                        page.Header()
                            .Height(30); // Damos un espacio para el encabezado aunque no tenga contenido repetitivo.
                            // Si quieres un logo o algo que se repita en cada encabezado, lo pones aquí:
                            // .Image("ruta/a/tu/logo.png").Height(20).AlignRight();

                        // Define el contenido principal de la página.
                        page.Content()
                            .PaddingVertical(20) // Añade relleno vertical para separar del encabezado/pie de página
                            .Column(col => // Organiza el contenido en una columna vertical
                            {
                                col.Spacing(12); // Espacio reducido entre elementos para aprovechar mejor el espacio
                                
                                // Espacio inicial para mejor presentación
                                col.Item().Height(8);

                                // Título principal del documento, que ahora SOLO aparecerá en la primera página
                                col.Item()
                                    .Row(row =>
                                    {
                                        // Título principal a la izquierda
                                        row.RelativeItem(3)
                                            .Column(titleCol =>
                                            {
                                                titleCol.Item()
                                                    .Text(varietyDetail.Name)
                                                    .FontSize(32)
                                                    .Bold()
                                                    .FontColor(Color.FromHex("#3F2E24")) // Color marrón oscuro de la imagen
                                                    .FontFamily("Calibri");
                                                
                                                titleCol.Item().Height(8); // Espacio entre título y nombre científico
                                                
                                                titleCol.Item()
                                                    .Text(varietyDetail.ScientificName)
                                                    .FontSize(14)
                                                    .Italic()
                                                    .FontColor(Color.FromHex("#3F2E24")) // Color marrón oscuro de la imagen
                                                    .FontFamily("Calibri");
                                                
                                                titleCol.Item().Height(12); // Espacio entre nombre científico y descripción
                                                
                                                // Descripción de la variedad (similar a la imagen de referencia)
                                                titleCol.Item()
                                                    .Text("Variedad de café con características agronómicas destacadas y excelente calidad de taza.")
                                                    .FontSize(12)
                                                    .FontColor(Color.FromHex("#3F2E24")) // Color marrón oscuro de la imagen
                                                    .FontFamily("Calibri");
                                            });
                                        
                                        // Círculo marrón a la derecha (donde irá la imagen de la variedad de café)
                                        row.RelativeItem(1)
                                            .AlignCenter()
                                            .AlignMiddle()
                                            .Width(60)
                                            .Height(60)
                                            .Background(Color.FromHex("#5C3F2B")); // Color marrón oscuro del círculo
                                    });

                                col.Item().Height(25); // Espacio optimizado después del título

                                // Sección: Agronomics (como en la imagen de referencia)
                                col.Item()
                                    .Text("Agronomics")
                                    .FontSize(20)
                                    .Bold()
                                    .FontColor(Color.FromHex("#3F2E24")) // Color marrón oscuro de la imagen
                                    .FontFamily("Calibri");
                                
                                col.Item().Table(table =>
                                {
                                    table.ColumnsDefinition(columns =>
                                    {
                                        columns.RelativeColumn(2);
                                        columns.RelativeColumn(3);
                                    });
                                    
                                    // Filas con colores alternados
                                    AddTableRow(table, "Porte (altura)", varietyDetail.PlantHeight, 0);
                                    AddTableRow(table, "Tamaño de grano", varietyDetail.BeanSize, 1);
                                    AddTableRow(table, "Potencial de rendimiento", varietyDetail.YieldPotential, 0);
                                    AddTableRow(table, "Tiempo de cosecha", varietyDetail.HarvestTime ?? "No disponible", 1);
                                    AddTableRow(table, "Tiempo de maduración", varietyDetail.MaturationTime ?? "No disponible", 0);
                                    AddTableRow(table, "Requerimiento nutricional", varietyDetail.NutritionalRequirement ?? "No disponible", 1);
                                });
                                
                                col.Item().Height(20); // Espacio optimizado entre secciones
                                
                                // Sección: Background (como en la imagen de referencia)
                                col.Item()
                                    .Text("Background")
                                    .FontSize(20)
                                    .Bold()
                                    .FontColor(Color.FromHex("#3F2E24")) // Color marrón oscuro de la imagen
                                    .FontFamily("Calibri");
                                
                                                            col.Item().Table(table =>
                            {
                                table.ColumnsDefinition(columns =>
                                {
                                    columns.RelativeColumn(2);
                                    columns.RelativeColumn(3);
                                });
                                
                                // Datos de fondo de la variedad (como en la imagen de referencia)
                                AddTableRow(table, "Origen", varietyDetail.LineageName ?? "No disponible", 0);
                                AddTableRow(table, "Historia", varietyDetail.History ?? "No disponible", 1);
                                AddTableRow(table, "Características", varietyDetail.SpeciesName ?? "No disponible", 0);
                            });
                                
                                col.Item().Height(20); // Espacio optimizado entre secciones
                                
                                // Sección: Condiciones de cultivo con tabla
                                col.Item()
                                    .Text("Condiciones de Cultivo")
                                    .FontSize(20)
                                    .Bold()
                                    .FontColor(Color.FromHex("#3F2E24")) // Color marrón oscuro de la imagen
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
                                        AddTableRow(table, "Densidad de siembra", $"{varietyDetail.PlantingDensityValue} {varietyDetail.PlantingDensityUnit}", 0);
                                    }
                                });
                            });

                        // Define el pie de página. Este se REPETIRÁ en cada página.
                        page.Footer()
                            .Padding(12)
                            .Row(row =>
                            {
                                // Logo a la izquierda (imagen del logo más pequeño)
                                row.RelativeItem(1)
                                    .AlignLeft()
                                    .AlignMiddle()
                                    .Height(50) // Contenedor con altura específica para logo pequeño
                                    .Image(Path.Combine(Directory.GetCurrentDirectory(), "Src", "assets", "logo.png"))
                                    .FitArea();
                                
                                // Información de generación al centro
                                row.RelativeItem(2)
                                    .AlignCenter()
                                    .AlignMiddle()
                                    .Column(centerCol =>
                                    {
                                        centerCol.Item()
                                            .Text("Elaborado por: Colombian Coffee System")
                                            .FontSize(10)
                                            .FontColor(Color.FromHex("#3F2E24")); // Color marrón oscuro de la imagen
                                        
                                        centerCol.Item()
                                            .Text(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"))
                                            .FontSize(10)
                                            .Bold()
                                            .FontColor(Color.FromHex("#3F2E24")); // Color marrón oscuro de la imagen
                                    });
                                
                                // Espacio a la derecha (se quitó el círculo marrón)
                                row.RelativeItem(1)
                                    .AlignRight()
                                    .AlignMiddle();
                            });
                    });
                })
                .GeneratePdf(outputPath); // Genera el archivo PDF en la ruta especificada
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
