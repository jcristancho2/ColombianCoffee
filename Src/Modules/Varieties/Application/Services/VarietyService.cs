using Microsoft.EntityFrameworkCore;
using ColombianCoffee.src.Modules.Varieties.Application.DTOs;
using ColombianCoffee.src.Modules.Varieties.Application.Interfaces;
using ColombianCoffee.src.Modules.Varieties.Domain.Entities;
using ColombianCoffee.src.Modules.Varieties.Infrastructure;
using ColombianCoffee.Src.Shared.Contexts;

namespace ColombianCoffee.src.Modules.Varieties.Application.Services
{
    public sealed class VarietyService : IVarietyService
    {
        private readonly IVarietyRepository _varietyRepository;
        private readonly AppDbContext _dbContext;
        public VarietyService(IVarietyRepository varietyRepository, AppDbContext dbContext)
        {
            _varietyRepository = varietyRepository;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<VarietyIdNameDto>> GetFilteredVarietiesAsync(
            VarietyFilterDto filters,
            CancellationToken ct = default)
        {
            var query = _varietyRepository.Query();

            // Aplicar filtros dinámicos
            if (!string.IsNullOrWhiteSpace(filters.NameContains))
                query = query.Where(v => v.Name.Contains(filters.NameContains));

            if (filters.SpeciesId.HasValue)
                query = query.Where(v => v.SpeciesId == filters.SpeciesId);

            if (filters.GeneticGroupId.HasValue)
                query = query.Where(v => v.GeneticGroupId == filters.GeneticGroupId);

            if (filters.LineageId.HasValue)
                query = query.Where(v => v.LineageId == filters.LineageId);

            if (!string.IsNullOrWhiteSpace(filters.PlantHeight))
                query = query.Where(v => v.PlantHeight == filters.PlantHeight);

            if (!string.IsNullOrWhiteSpace(filters.BeanSize))
                query = query.Where(v => v.BeanSize == filters.BeanSize);

            if (!string.IsNullOrWhiteSpace(filters.YieldPotential))
                query = query.Where(v => v.YieldPotential == filters.YieldPotential);

            if (!string.IsNullOrWhiteSpace(filters.RustResistance))
                query = query.Where(v => v.RustResistance == filters.RustResistance);

            if (!string.IsNullOrWhiteSpace(filters.AnthracnoseResistance))
                query = query.Where(v => v.AnthracnoseResistance == filters.AnthracnoseResistance);

            if (!string.IsNullOrWhiteSpace(filters.NematodesResistance))
                query = query.Where(v => v.NematodesResistance == filters.NematodesResistance);

            if (filters.MinAltitude.HasValue)
                query = query.Where(v => v.MinAltitude >= filters.MinAltitude);

            if (filters.MaxAltitude.HasValue)
                query = query.Where(v => v.MaxAltitude <= filters.MaxAltitude);

            if (filters.AltitudeQualityId.HasValue)
                query = query.Where(v => v.AltitudeQualityId == filters.AltitudeQualityId);

            // Proyección mínima: solo Id y Nombre
            return await query
                .Select(v => new VarietyIdNameDto
                {
                    Id = v.Id,
                    Name = v.Name
                })
                .ToListAsync(ct);
        }

        public async Task<VarietyDetailDto?> GetVarietyDetailAsync(
            uint id,
            CancellationToken ct = default)
        {
            var variety = await _varietyRepository.GetByIdAsync(id);

            if (variety == null)
                return null;

            return new VarietyDetailDto
            {
                Id = variety.Id,
                Name = variety.Name,
                ScientificName = variety.ScientificName,
                History = variety.History,
                SpeciesName = variety.Species.CommonName,
                GeneticGroupName = variety.GeneticGroup.Name,
                LineageName = variety.Lineage.Name,
                PlantHeight = variety.PlantHeight,
                BeanSize = variety.BeanSize,
                YieldPotential = variety.YieldPotential,
                HarvestTime = variety.HarvestTime,
                MaturationTime = variety.MaturationTime,
                NutritionalRequirement = variety.NutritionalRequirement,
                RustResistance = variety.RustResistance,
                AnthracnoseResistance = variety.AnthracnoseResistance,
                NematodesResistance = variety.NematodesResistance,
                MinAltitude = variety.MinAltitude,
                MaxAltitude = variety.MaxAltitude,
                AltitudeQualityLabel = variety.AltitudeQuality.Label,
                PlantingDensityValue = variety.PlantingDensityValue,
                PlantingDensityUnit = variety.PlantingDensityUnit?.Name,
                AltitudeUnit = variety.AltitudeUnit?.Name,
                ImageUrl = variety.ImageUrl
            };
        }
        public async Task<VarietyDetailDto> CreateVarietyAsync(VarietyDetailDto varietyDto)
        {
            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(varietyDto.Name))
                throw new ArgumentException("El nombre de la variedad es requerido");

            if (varietyDto.MinAltitude >= varietyDto.MaxAltitude)
                throw new ArgumentException("La altitud mínima debe ser menor que la máxima");

            // Buscar entidades relacionadas
            var species = await _dbContext.Species.FirstOrDefaultAsync(s => s.CommonName == varietyDto.SpeciesName);
            var geneticGroup = await _dbContext.GeneticGroup.FirstOrDefaultAsync(g => g.Name == varietyDto.GeneticGroupName);
            var lineage = await _dbContext.Lineage.FirstOrDefaultAsync(l => l.Name == varietyDto.LineageName);
            var altitudeQuality = await _dbContext.AltitudeQuality.FirstOrDefaultAsync(a => a.Label == varietyDto.AltitudeQualityLabel);

            if (species == null || geneticGroup == null || lineage == null || altitudeQuality == null)
                throw new ArgumentException("Datos de referencia no válidos");

            // Crear nueva entidad
            var variety = new Variety
            {
                Name = varietyDto.Name,
                ScientificName = varietyDto.ScientificName,
                History = varietyDto.History,
                SpeciesId = species.Id,
                GeneticGroupId = geneticGroup.Id,
                LineageId = lineage.Id,
                PlantHeight = varietyDto.PlantHeight,
                BeanSize = varietyDto.BeanSize,
                YieldPotential = varietyDto.YieldPotential,
                RustResistance = varietyDto.RustResistance,
                AnthracnoseResistance = varietyDto.AnthracnoseResistance,
                NematodesResistance = varietyDto.NematodesResistance,
                MinAltitude = varietyDto.MinAltitude,
                MaxAltitude = varietyDto.MaxAltitude,
                AltitudeQualityId = altitudeQuality.Id,
                HarvestTime = varietyDto.HarvestTime,
                MaturationTime = varietyDto.MaturationTime,
                NutritionalRequirement = varietyDto.NutritionalRequirement,
                PlantingDensityValue = varietyDto.PlantingDensityValue,
                ImageUrl = varietyDto.ImageUrl
            };

            // Manejar unidades de medida si existen
            if (!string.IsNullOrWhiteSpace(varietyDto.PlantingDensityUnit))
            {
                var plantingDensityUnit = await _dbContext.MeasurementUnit
                    .FirstOrDefaultAsync(m => m.Name == varietyDto.PlantingDensityUnit);
                
                if (plantingDensityUnit != null)
                    variety.PlantingDensityUnitId = plantingDensityUnit.Id;
            }

            if (!string.IsNullOrWhiteSpace(varietyDto.AltitudeUnit))
            {
                var altitudeUnit = await _dbContext.MeasurementUnit
                    .FirstOrDefaultAsync(m => m.Name == varietyDto.AltitudeUnit);
                
                if (altitudeUnit != null)
                    variety.AltitudeUnitId = altitudeUnit.Id;
            }

                _dbContext.Varieties.Add(variety);
                await _dbContext.SaveChangesAsync();

            return await GetVarietyDetailAsync(variety.Id);
        }

        public async Task<VarietyDetailDto> UpdateVarietyAsync(uint id, VarietyDetailDto varietyDto)
        {
            var existingVariety = await _varietyRepository.GetByIdAsync(id);
            if (existingVariety == null)
                throw new KeyNotFoundException("Variedad no encontrada");

            // Validaciones básicas
            if (string.IsNullOrWhiteSpace(varietyDto.Name))
                throw new ArgumentException("El nombre de la variedad es requerido");

            if (varietyDto.MinAltitude >= varietyDto.MaxAltitude)
                throw new ArgumentException("La altitud mínima debe ser menor que la máxima");

            // Buscar entidades relacionadas
            var species = await _dbContext.Species.FirstOrDefaultAsync(s => s.CommonName == varietyDto.SpeciesName);
            var geneticGroup = await _dbContext.GeneticGroup.FirstOrDefaultAsync(g => g.Name == varietyDto.GeneticGroupName);
            var lineage = await _dbContext.Lineage.FirstOrDefaultAsync(l => l.Name == varietyDto.LineageName);
            var altitudeQuality = await _dbContext.AltitudeQuality.FirstOrDefaultAsync(a => a.Label == varietyDto.AltitudeQualityLabel);

            if (species == null || geneticGroup == null || lineage == null || altitudeQuality == null)
                throw new ArgumentException("Datos de referencia no válidos");

            // Actualizar propiedades
            existingVariety.Name = varietyDto.Name;
            existingVariety.ScientificName = varietyDto.ScientificName;
            existingVariety.History = varietyDto.History;
            existingVariety.SpeciesId = species.Id;
            existingVariety.GeneticGroupId = geneticGroup.Id;
            existingVariety.LineageId = lineage.Id;
            existingVariety.PlantHeight = varietyDto.PlantHeight;
            existingVariety.BeanSize = varietyDto.BeanSize;
            existingVariety.YieldPotential = varietyDto.YieldPotential;
            existingVariety.RustResistance = varietyDto.RustResistance;
            existingVariety.AnthracnoseResistance = varietyDto.AnthracnoseResistance;
            existingVariety.NematodesResistance = varietyDto.NematodesResistance;
            existingVariety.MinAltitude = varietyDto.MinAltitude;
            existingVariety.MaxAltitude = varietyDto.MaxAltitude;
            existingVariety.AltitudeQualityId = altitudeQuality.Id;
            existingVariety.HarvestTime = varietyDto.HarvestTime;
            existingVariety.MaturationTime = varietyDto.MaturationTime;
            existingVariety.NutritionalRequirement = varietyDto.NutritionalRequirement;
            existingVariety.PlantingDensityValue = varietyDto.PlantingDensityValue;
            existingVariety.ImageUrl = varietyDto.ImageUrl;

            // Manejar unidades de medida
            if (!string.IsNullOrWhiteSpace(varietyDto.PlantingDensityUnit))
            {
                var plantingDensityUnit = await _dbContext.MeasurementUnit
                    .FirstOrDefaultAsync(m => m.Name == varietyDto.PlantingDensityUnit);
                
                existingVariety.PlantingDensityUnitId = plantingDensityUnit?.Id;
            }
            else
            {
                existingVariety.PlantingDensityUnitId = null;
            }

            if (!string.IsNullOrWhiteSpace(varietyDto.AltitudeUnit))
            {
                var altitudeUnit = await _dbContext.MeasurementUnit
                    .FirstOrDefaultAsync(m => m.Name == varietyDto.AltitudeUnit);
                
                existingVariety.AltitudeUnitId = altitudeUnit?.Id;
            }
            else
            {
                existingVariety.AltitudeUnitId = null;
            }

                await _dbContext.SaveChangesAsync();

            return await GetVarietyDetailAsync(existingVariety.Id);
        }

        public async Task<bool> DeleteVarietyAsync(uint id)
        {
            var variety = await _varietyRepository.GetByIdAsync(id);
            if (variety == null)
                return false;

            _dbContext.Varieties.Remove(variety);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
 