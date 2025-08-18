using Microsoft.EntityFrameworkCore;
using ColombianCoffee.src.Modules.Varieties.Application.DTOs;
using ColombianCoffee.src.Modules.Varieties.Application.Interfaces;
using ColombianCoffee.src.Modules.Varieties.Domain.Entities;

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
                SpeciesName = variety.Species?.CommonName ?? string.Empty,
                GeneticGroupName = variety.GeneticGroup?.Name ?? string.Empty,
                LineageName = variety.Lineage?.Name ?? string.Empty,
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
                AltitudeQualityLabel = variety.AltitudeQuality?.Label ?? string.Empty,
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

            // Crear nueva entidad
            var variety = new Variety
            {
                Name = varietyDto.Name,
                ScientificName = varietyDto.ScientificName,
                History = string.IsNullOrWhiteSpace(varietyDto.History) ? null : varietyDto.History,
                SpeciesId = await GetSpeciesIdByName(varietyDto.SpeciesName),
                GeneticGroupId = await GetGeneticGroupIdByName(varietyDto.GeneticGroupName),
                LineageId = await GetLineageIdByName(varietyDto.LineageName),
                PlantHeight = varietyDto.PlantHeight,
                BeanSize = varietyDto.BeanSize,
                YieldPotential = varietyDto.YieldPotential,
                RustResistance = varietyDto.RustResistance,
                AnthracnoseResistance = varietyDto.AnthracnoseResistance,
                NematodesResistance = varietyDto.NematodesResistance,
                MinAltitude = varietyDto.MinAltitude,
                MaxAltitude = varietyDto.MaxAltitude,
                AltitudeQualityId = await GetAltitudeQualityIdByLabel(varietyDto.AltitudeQualityLabel),
                HarvestTime = string.IsNullOrWhiteSpace(varietyDto.HarvestTime) ? null : varietyDto.HarvestTime,
                MaturationTime = string.IsNullOrWhiteSpace(varietyDto.MaturationTime) ? null : varietyDto.MaturationTime,
                NutritionalRequirement = string.IsNullOrWhiteSpace(varietyDto.NutritionalRequirement) ? null : varietyDto.NutritionalRequirement,
                PlantingDensityValue = varietyDto.PlantingDensityValue,
                ImageUrl = string.IsNullOrWhiteSpace(varietyDto.ImageUrl) ? null : varietyDto.ImageUrl
            };

            // Manejar unidades de medida si existen
            if (!string.IsNullOrWhiteSpace(varietyDto.PlantingDensityUnit))
            {
                variety.PlantingDensityUnitId = await GetMeasurementUnitIdByName(varietyDto.PlantingDensityUnit);
            }

            if (!string.IsNullOrWhiteSpace(varietyDto.AltitudeUnit))
            {
                variety.AltitudeUnitId = await GetMeasurementUnitIdByName(varietyDto.AltitudeUnit);
            }

            // Guardar usando el repositorio
            var savedVariety = await _varietyRepository.CreateAsync(variety);
            return await GetVarietyDetailAsync(savedVariety.Id);
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

            // Actualizar propiedades
            existingVariety.Name = varietyDto.Name;
            existingVariety.ScientificName = varietyDto.ScientificName;
            existingVariety.History = string.IsNullOrWhiteSpace(varietyDto.History) ? null : varietyDto.History;
            existingVariety.SpeciesId = await GetSpeciesIdByName(varietyDto.SpeciesName);
            existingVariety.GeneticGroupId = await GetGeneticGroupIdByName(varietyDto.GeneticGroupName);
            existingVariety.LineageId = await GetLineageIdByName(varietyDto.LineageName);
            existingVariety.PlantHeight = varietyDto.PlantHeight;
            existingVariety.BeanSize = varietyDto.BeanSize;
            existingVariety.YieldPotential = varietyDto.YieldPotential;
            existingVariety.RustResistance = varietyDto.RustResistance;
            existingVariety.AnthracnoseResistance = varietyDto.AnthracnoseResistance;
            existingVariety.NematodesResistance = varietyDto.NematodesResistance;
            existingVariety.MinAltitude = varietyDto.MinAltitude;
            existingVariety.MaxAltitude = varietyDto.MaxAltitude;
            existingVariety.AltitudeQualityId = await GetAltitudeQualityIdByLabel(varietyDto.AltitudeQualityLabel);
            existingVariety.HarvestTime = string.IsNullOrWhiteSpace(varietyDto.HarvestTime) ? null : varietyDto.HarvestTime;
            existingVariety.MaturationTime = string.IsNullOrWhiteSpace(varietyDto.MaturationTime) ? null : varietyDto.MaturationTime;
            existingVariety.NutritionalRequirement = string.IsNullOrWhiteSpace(varietyDto.NutritionalRequirement) ? null : varietyDto.NutritionalRequirement;
            existingVariety.PlantingDensityValue = varietyDto.PlantingDensityValue;
            existingVariety.ImageUrl = string.IsNullOrWhiteSpace(varietyDto.ImageUrl) ? null : varietyDto.ImageUrl;

            // Manejar unidades de medida
            if (!string.IsNullOrWhiteSpace(varietyDto.PlantingDensityUnit))
            {
                existingVariety.PlantingDensityUnitId = await GetMeasurementUnitIdByName(varietyDto.PlantingDensityUnit);
            }
            else
            {
                existingVariety.PlantingDensityUnitId = null;
            }

            if (!string.IsNullOrWhiteSpace(varietyDto.AltitudeUnit))
            {
                existingVariety.AltitudeUnitId = await GetMeasurementUnitIdByName(varietyDto.AltitudeUnit);
            }
            else
            {
                existingVariety.AltitudeUnitId = null;
            }

            // Actualizar usando el repositorio
            await _varietyRepository.UpdateAsync(existingVariety);
            return await GetVarietyDetailAsync(existingVariety.Id);
        }

        public async Task<bool> DeleteVarietyAsync(uint id)
        {
            var variety = await _varietyRepository.GetByIdAsync(id);
            if (variety == null)
                return false;

            return await _varietyRepository.DeleteAsync(id);
        }

        // Métodos auxiliares para obtener IDs de entidades relacionadas
        private async Task<uint> GetSpeciesIdByName(string speciesName)
        {
            var species = await _varietyRepository.GetSpeciesByNameAsync(speciesName);
            if (species == null)
                throw new ArgumentException($"Especie '{speciesName}' no encontrada");
            return species.Id;
        }

        private async Task<int> GetGeneticGroupIdByName(string geneticGroupName)
        {
            var geneticGroup = await _varietyRepository.GetGeneticGroupByNameAsync(geneticGroupName);
            if (geneticGroup == null)
                throw new ArgumentException($"Grupo genético '{geneticGroupName}' no encontrado");
            return geneticGroup.Id;
        }

        private async Task<int> GetLineageIdByName(string lineageName)
        {
            var lineage = await _varietyRepository.GetLineageByNameAsync(lineageName);
            if (lineage == null)
                throw new ArgumentException($"Linaje '{lineageName}' no encontrado");
            return lineage.Id;
        }

        private async Task<uint> GetAltitudeQualityIdByLabel(string label)
        {
            var altitudeQuality = await _varietyRepository.GetAltitudeQualityByLabelAsync(label);
            if (altitudeQuality == null)
                throw new ArgumentException($"Calidad de altitud '{label}' no encontrada");
            return altitudeQuality.Id;
        }

        private async Task<uint?> GetMeasurementUnitIdByName(string unitName)
        {
            var unit = await _varietyRepository.GetMeasurementUnitByNameAsync(unitName);
            return unit?.Id;
        }

        // Métodos para obtener opciones de catálogos
        public async Task<IEnumerable<string>> GetSpeciesOptionsAsync()
        {
            var species = await _varietyRepository.GetAllSpeciesAsync();
            return species.Select(s => s.CommonName);
        }

        public async Task<IEnumerable<string>> GetGeneticGroupOptionsAsync()
        {
            var geneticGroups = await _varietyRepository.GetAllGeneticGroupsAsync();
            return geneticGroups.Select(g => g.Name);
        }

        public async Task<IEnumerable<string>> GetLineageOptionsAsync()
        {
            var lineages = await _varietyRepository.GetAllLineagesAsync();
            return lineages.Select(l => l.Name);
        }

        public async Task<IEnumerable<string>> GetAltitudeQualityOptionsAsync()
        {
            var altitudeQualities = await _varietyRepository.GetAllAltitudeQualitiesAsync();
            return altitudeQualities.Select(a => a.Label);
        }
    }
}
 