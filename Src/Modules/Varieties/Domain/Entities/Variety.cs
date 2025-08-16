using ColombianCoffee.Modules.Varieties.Domain.Entities;

namespace ColombianCoffee.src.Modules.Varieties.Domain.Entities
{
    public class Variety
    {
        public uint Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ScientificName { get; set; } = string.Empty;
        public string? History { get; set; }

        public string PlantHeight { get; set; } = string.Empty;       // Alto, Medio, Bajo
        public string BeanSize { get; set; } = string.Empty;          // Pequeño, Medio, Grande
        public string YieldPotential { get; set; } = string.Empty;    // Muy bajo ... Excepcional
        public string RustResistance { get; set; } = string.Empty;
        public string AnthracnoseResistance { get; set; } = string.Empty;
        public string NematodesResistance { get; set; } = string.Empty;

        public string? NutritionalRequirement { get; set; }
        public int MinAltitude { get; set; }
        public int MaxAltitude { get; set; }

        public string? HarvestTime { get; set; }
        public string? MaturationTime { get; set; }
        
        public decimal? PlantingDensityValue { get; set; }
        public uint? PlantingDensityUnitId { get; set; }
        public uint? AltitudeUnitId { get; set; }
        
        public string? ImageUrl { get; set; }

        // Relaciones para joins si quieres mostrar más info
        public uint SpeciesId { get; set; }
        public Species? Species { get; set; }

        public int? GeneticGroupId { get; set; }
        public GeneticGroup? GeneticGroup { get; set; }

        public int? LineageId { get; set; }
        public Lineage? Lineage { get; set; }

        public uint AltitudeQualityId { get; set; }
        public AltitudeQuality? AltitudeQuality { get; set; }
        
        // Relaciones de navegación para unidades de medida
        public MeasurementUnit? PlantingDensityUnit { get; set; }
        public MeasurementUnit? AltitudeUnit { get; set; }
    }
}
