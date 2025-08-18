namespace ColombianCoffee.Modules.Varieties.Domain.Entities;

public class Species
{
    public uint Id { get; set; }
    public string ScientificName { get; set; } = string.Empty;
    public string CommonName { get; set; } = string.Empty;
    public string? Description { get; set; }
}