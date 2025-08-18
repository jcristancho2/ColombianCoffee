using ColombianCoffee.Modules.Varieties.Domain.Entities;

namespace ColombianCoffee.src.Modules.Varieties.Application.Interfaces
{
    public interface IVarietyRepository
    {
        IQueryable<Variety> Query();

        Task<Variety?> GetByIdAsync(uint id);
        
        Task<Variety> CreateAsync(Variety variety);
        
        Task UpdateAsync(Variety variety);
        
        Task<bool> DeleteAsync(uint id);
        
        // Métodos para obtener entidades relacionadas
        Task<Species?> GetSpeciesByNameAsync(string name);
        
        Task<GeneticGroup?> GetGeneticGroupByNameAsync(string name);
        
        Task<Lineage?> GetLineageByNameAsync(string name);
        
        Task<AltitudeQuality?> GetAltitudeQualityByLabelAsync(string label);
        
        Task<MeasurementUnit?> GetMeasurementUnitByNameAsync(string name);
        
        // Métodos para obtener todas las entidades de catálogo
        Task<IEnumerable<Species>> GetAllSpeciesAsync();
        
        Task<IEnumerable<GeneticGroup>> GetAllGeneticGroupsAsync();
        
        Task<IEnumerable<Lineage>> GetAllLineagesAsync();
        
        Task<IEnumerable<AltitudeQuality>> GetAllAltitudeQualitiesAsync();
        
        Task<IEnumerable<MeasurementUnit>> GetAllMeasurementUnitsAsync();
    }
}