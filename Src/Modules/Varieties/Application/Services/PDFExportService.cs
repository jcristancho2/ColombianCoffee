using Microsoft.Extensions.Logging;
using QuestPDF.Infrastructure;
using ColombianCoffee.Src.Modules.Varieties.Application.Interfaces;
using ColombianCoffee.Modules.Varieties.Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using FluentResults;


public class PDFExportService : IPDFExportService
{
    private readonly ILogger<PDFExportService> _logger;
    private readonly IWebHostEnvironment _env;

    public PDFExportService(IWebHostEnvironment env, ILogger<PDFExportService> logger)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        _env = env;
        _logger = logger;
    }

    public void ExportVariedad(Variety variedad, string filePath)
    {
        throw new NotImplementedException();
    }

    public async Task<Result<string>> ExportVariedadAsync(Variety variedad, string directoryPath)
    {
        try
        {
            if (variedad == null)
                return Result.Fail<string>("La variedad no puede ser nula");

            if (!Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);

            string fileName = $"Variedad_{variedad.Id}_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            string fullPath = Path.Combine(directoryPath, fileName);

            var document = Document.Create(container =>
            {
                // ... (resto del código igual)
            });

            await Task.Run(() => document.GeneratePdf(fullPath));
            
            _logger.LogInformation($"PDF generado: {fullPath}");
            return Result.Ok(fullPath);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generando PDF");
            return Result.Fail<string>($"Error generando PDF: {ex.Message}");
        }
    }
    
}

