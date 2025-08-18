using Microsoft.EntityFrameworkCore;
using ColombianCoffee.src.Modules.Varieties.Application.Interfaces;
using ColombianCoffee.src.Modules.Varieties.Domain.Entities;
using ColombianCoffee.Src.Shared.Contexts;

namespace ColombianCoffee.src.Modules.Varieties.Infrastructure;

public class VarietyRepository : IVarietyRepository
{
    private readonly AppDbContext _dbContext;

    public VarietyRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IQueryable<Variety> Query()
    {
        return _dbContext.Varieties.AsNoTracking()
            .Include(v => v.Species)
            .Include(v => v.GeneticGroup)
            .Include(v => v.Lineage)
            .Include(v => v.AltitudeQuality)
            .Include(v => v.PlantingDensityUnit)
            .Include(v => v.AltitudeUnit);
    }

    public async Task<Variety?> GetByIdAsync(uint id)
    {
        return await _dbContext.Varieties
            .AsNoTracking()
            .Include(v => v.Species)
            .Include(v => v.GeneticGroup)
            .Include(v => v.Lineage)
            .Include(v => v.AltitudeQuality)
            .Include(v => v.PlantingDensityUnit)
            .Include(v => v.AltitudeUnit)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<Variety> CreateAsync(Variety variety)
    {
        _dbContext.Varieties.Add(variety);
        await _dbContext.SaveChangesAsync();
        return variety;
    }

    public async Task UpdateAsync(Variety variety)
    {
        _dbContext.Varieties.Update(variety);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(uint id)
    {
        var variety = await _dbContext.Varieties.FindAsync(id);
        if (variety == null)
            return false;

        _dbContext.Varieties.Remove(variety);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<Species?> GetSpeciesByNameAsync(string name)
    {
        return await _dbContext.Species
            .AsNoTracking()
            .FirstOrDefaultAsync(s => s.CommonName == name);
    }

    public async Task<GeneticGroup?> GetGeneticGroupByNameAsync(string name)
    {
        return await _dbContext.GeneticGroup
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Name == name);
    }

    public async Task<Lineage?> GetLineageByNameAsync(string name)
    {
        return await _dbContext.Lineage
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Name == name);
    }

    public async Task<AltitudeQuality?> GetAltitudeQualityByLabelAsync(string label)
    {
        return await _dbContext.AltitudeQuality
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Label == label);
    }

    public async Task<MeasurementUnit?> GetMeasurementUnitByNameAsync(string name)
    {
        return await _dbContext.MeasurementUnit
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Name == name);
    }

    // Métodos para obtener todas las entidades de catálogo
    public async Task<IEnumerable<Species>> GetAllSpeciesAsync()
    {
        return await _dbContext.Species
            .AsNoTracking()
            .OrderBy(s => s.CommonName)
            .ToListAsync();
    }

    public async Task<IEnumerable<GeneticGroup>> GetAllGeneticGroupsAsync()
    {
        return await _dbContext.GeneticGroup
            .AsNoTracking()
            .OrderBy(g => g.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Lineage>> GetAllLineagesAsync()
    {
        return await _dbContext.Lineage
            .AsNoTracking()
            .OrderBy(l => l.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<AltitudeQuality>> GetAllAltitudeQualitiesAsync()
    {
        return await _dbContext.AltitudeQuality
            .AsNoTracking()
            .OrderBy(a => a.Score)
            .ToListAsync();
    }

    public async Task<IEnumerable<MeasurementUnit>> GetAllMeasurementUnitsAsync()
    {
        return await _dbContext.MeasurementUnit
            .AsNoTracking()
            .OrderBy(m => m.Name)
            .ToListAsync();
    }
}