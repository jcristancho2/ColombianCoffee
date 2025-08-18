namespace ColombianCoffee.src.Modules.Varieties.Application.DTOs;

public sealed class VarietyDetailDto
{
    public uint Id { get; set; }

    // Identidad
    public string Name { get; set; } = string.Empty;
    public string ScientificName { get; set; } = string.Empty;
    public string? History { get; set; }

    // Taxonomía y linaje
    public string SpeciesName { get; set; } = string.Empty;
    public string GeneticGroupName { get; set; } = string.Empty;
    public string LineageName { get; set; } = string.Empty;

    // Atributos agronómicos
    public string PlantHeight { get; set; } = string.Empty;
    public string BeanSize { get; set; } = string.Empty;
    public string YieldPotential { get; set; } = string.Empty;
    public string? HarvestTime { get; set; }
    public string? MaturationTime { get; set; }
    public string? NutritionalRequirement { get; set; }

    // Resistencias
    public string RustResistance { get; set; } = string.Empty;
    public string AnthracnoseResistance { get; set; } = string.Empty;
    public string NematodesResistance { get; set; } = string.Empty;

    // Altitud y calidad
    public int MinAltitude { get; set; }
    public int MaxAltitude { get; set; }
    public string AltitudeQualityLabel { get; set; } = string.Empty;

    // Densidad de siembra
    public decimal? PlantingDensityValue { get; set; }
    public string? PlantingDensityUnit { get; set; }
    
    // Unidad de altitud
    public string? AltitudeUnit { get; set; }

    // Medios
    public string? ImageUrl { get; set; }
}
