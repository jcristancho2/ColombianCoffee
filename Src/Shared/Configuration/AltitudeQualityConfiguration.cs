using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ColombianCoffee.Src.Modules.Varieties.Domain.Entities;

namespace ColombianCoffee.Src.Shared.Configuration;

public class AltitudeQualityConfiguration : IEntityTypeConfiguration<AltitudeQuality>
{
    public void Configure(EntityTypeBuilder<AltitudeQuality> b)
    {
        b.ToTable("altitude_quality");
        b.HasKey(x => x.Id);
        b.Property(x => x.Id).HasColumnName("id");
        b.Property(x => x.Label).HasColumnName("label").HasMaxLength(50).IsRequired();
        b.Property(x => x.Score).HasColumnName("score").HasColumnType("tinyint unsigned");
        b.HasIndex(x => x.Label).IsUnique();
    }
}
