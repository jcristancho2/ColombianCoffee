namespace ColombianCoffee.Src.Modules.Varieties.Application.DTOs;

public sealed class VarietyFilterDto
{
    public string? NameContains { get; set; }
    public uint? SpeciesId { get; set; }
    public int? GeneticGroupId { get; set; }
    public int? LineageId { get; set; }
    public string? PlantHeight { get; set; }
    public string? BeanSize { get; set; }
    public string? YieldPotential { get; set; }
    public string? RustResistance { get; set; }
    public string? AnthracnoseResistance { get; set; }
    public string? NematodesResistance { get; set; }
    public int? MinAltitude { get; set; }
    public int? MaxAltitude { get; set; }
    public uint? AltitudeQualityId { get; set; }
}
