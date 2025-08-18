namespace ColombianCoffee.Src.Modules.Varieties.Domain.Entities;

public class GeneticGroup
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string? Notes { get; set; }
}