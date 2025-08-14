namespace ColombianCoffee.src.Modules.Varieties.Domain.Entities;

public class AltitudeQuality
{
    public uint Id { get; set; }
    public string Label { get; set; } = string.Empty;
    public byte Score { get; set; }
}