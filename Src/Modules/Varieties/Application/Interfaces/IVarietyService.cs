using ColombianCoffee.Src.Modules.Varieties.Application.DTOs;

namespace ColombianCoffee.Src.Modules.Varieties.Application.Interfaces;

public interface IVarietyService
{
    Task<IEnumerable<VarietyIdNameDto>> GetFilteredVarietiesAsync(VarietyFilterDto filter, CancellationToken ct = default);
    Task<VarietyDetailDto?> GetVarietyDetailAsync(uint id, CancellationToken ct = default);
    Task<VarietyDetailDto> CreateVarietyAsync(VarietyDetailDto varietyDto);
    Task<VarietyDetailDto> UpdateVarietyAsync(uint id, VarietyDetailDto varietyDto);
    Task<bool> DeleteVarietyAsync(uint id);
    
    // Métodos para obtener opciones de catálogos
    Task<IEnumerable<string>> GetSpeciesOptionsAsync();
    Task<IEnumerable<string>> GetGeneticGroupOptionsAsync();
    Task<IEnumerable<string>> GetLineageOptionsAsync();
    Task<IEnumerable<string>> GetAltitudeQualityOptionsAsync();
}
