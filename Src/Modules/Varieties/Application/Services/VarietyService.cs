using Microsoft.EntityFrameworkCore;
using ColombianCoffee.src.Modules.Varieties.Application.DTOs;
using ColombianCoffee.src.Modules.Varieties.Application.Interfaces;

namespace ColombianCoffee.src.Modules.Varieties.Application.Services
{
    public sealed class VarietyService : IVarietyService
    {
        private readonly IVarietyRepository _varietyRepository;

        public VarietyService(IVarietyRepository varietyRepository)
        {
            _varietyRepository = varietyRepository;
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
    }
}
