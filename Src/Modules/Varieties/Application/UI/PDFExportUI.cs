using System.Diagnostics;
using ColombianCoffee.Src.Modules.Varieties.Application.Interfaces;
using Microsoft.Extensions.Logging;

public class PDFExportUI
{
    private readonly IPDFExportService _pdfExportService;
    private readonly IVarietyRepository _variedadRepo;
    private readonly ILogger<PDFExportUI> _logger;

    public PDFExportUI(
        IPDFExportService pdfExportService, 
        IVarietyRepository variedadRepo,
        ILogger<PDFExportUI> logger)
    {
        _pdfExportService = pdfExportService;
        _variedadRepo = variedadRepo;
        _logger = logger;
    }

    public async Task ShowAsync()
    {
        try
        {
            Console.WriteLine("Ingrese el ID de la variedad:");
            if (!uint.TryParse(Console.ReadLine(), out uint id))
            {
                Console.WriteLine("ID inválido. Debe ser un número.");
                return;
            }

            var variedad = await _variedadRepo.GetByIdAsync(id);
            if (variedad == null)
            {
                Console.WriteLine("Variedad no encontrada.");
                return;
            }

            // Obtener directorio de documentos del usuario
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string exportDirectory = Path.Combine(documentsPath, "ColombianCoffee", "Exports");
            
            var result = await _pdfExportService.ExportVariedadAsync(variedad, exportDirectory);

            if (result.IsSuccess)
            {
                Console.WriteLine($"PDF generado exitosamente en: {result.Value}");
                Console.WriteLine("¿Desea abrir el archivo? (S/N)");
                if (Console.ReadLine()?.ToUpper() == "S")
                {
                    Process.Start(new ProcessStartInfo(result.Value) { UseShellExecute = true });
                }
            }
            else
            {
                Console.WriteLine($"Error: {result.Errors.FirstOrDefault()?.Message ?? "Error desconocido"}");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error en la interfaz de usuario");
            Console.WriteLine("Ocurrió un error inesperado. Por favor revise el log.");
        }
    }
}