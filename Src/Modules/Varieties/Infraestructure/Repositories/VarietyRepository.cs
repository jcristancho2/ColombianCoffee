using Microsoft.EntityFrameworkCore;
using ColombianCoffee.src.Modules.Varieties.Application.Interfaces;
using ColombianCoffee.Modules.Varieties.Domain.Entities;
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
}