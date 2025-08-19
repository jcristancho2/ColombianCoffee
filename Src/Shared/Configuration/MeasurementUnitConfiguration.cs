using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ColombianCoffee.Src.Modules.Varieties.Domain.Entities;

namespace ColombianCoffee.Src.Shared.Configuration;

public class MeasurementUnitConfiguration : IEntityTypeConfiguration<MeasurementUnit>
{
    public void Configure(EntityTypeBuilder<MeasurementUnit> b)
    {
        b.ToTable("measurement_unit");

        b.HasKey(m => m.Id);
        b.Property(m => m.Id)
            .HasColumnName("id")
            .IsRequired();

        b.Property(m => m.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();
    }
}
