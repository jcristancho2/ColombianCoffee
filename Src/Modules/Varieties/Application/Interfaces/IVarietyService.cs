using ColombianCoffee.src.Modules.Varieties.Application.DTOs;

namespace ColombianCoffee.src.Modules.Varieties.Application.Interfaces;

public interface IVarietyService
{
    Task<IEnumerable<VarietyIdNameDto>> GetFilteredVarietiesAsync(VarietyFilterDto filter, CancellationToken ct = default);
    Task<VarietyDetailDto?> GetVarietyDetailAsync(uint id, CancellationToken ct = default);


}
