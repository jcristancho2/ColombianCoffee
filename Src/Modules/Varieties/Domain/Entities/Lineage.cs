namespace ColombianCoffee.src.Modules.Varieties.Domain.Entities;

public class Lineage
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string? Notes { get; set; }
}