using ColombianCoffee.src.Modules.Varieties.Application.DTOs;

namespace ColombianCoffee.src.Modules.Varieties.Application.Interfaces;

public interface IVarietyService
{
    Task<IEnumerable<VarietyIdNameDto>> GetFilteredVarietiesAsync(VarietyFilterDto filter, CancellationToken ct = default);
    Task<VarietyDetailDto?> GetVarietyDetailAsync(uint id, CancellationToken ct = default);

    // Nuevos métodos
    Task<string> ExportVarietyToPdfAsync(uint varietyId, string outputDirectory = "Exports");
    Task<string> ExportFilteredVarietiesToPdfAsync(VarietyFilterDto filters, string outputDirectory = "Exports", CancellationToken ct = default);
}
