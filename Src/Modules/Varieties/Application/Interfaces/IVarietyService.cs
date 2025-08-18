using ColombianCoffee.src.Modules.Varieties.Application.DTOs;

namespace ColombianCoffee.src.Modules.Varieties.Application.Interfaces;

public interface IVarietyService
{
    Task<IEnumerable<VarietyIdNameDto>> GetFilteredVarietiesAsync(VarietyFilterDto filter, CancellationToken ct = default);
    Task<VarietyDetailDto?> GetVarietyDetailAsync(uint id, CancellationToken ct = default);
    Task<VarietyDetailDto> CreateVarietyAsync(VarietyDetailDto varietyDto);
    Task<VarietyDetailDto> UpdateVarietyAsync(uint id, VarietyDetailDto varietyDto);
    Task<bool> DeleteVarietyAsync(uint id);
}
