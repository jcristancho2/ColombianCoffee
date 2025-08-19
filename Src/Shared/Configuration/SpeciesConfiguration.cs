using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ColombianCoffee.Src.Modules.Varieties.Domain.Entities;

namespace ColombianCoffee.Src.Shared.Configuration;

public class SpeciesConfiguration : IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> b)
    {
        b.ToTable("species");
        b.HasKey(x => x.Id);
        b.Property(x => x.Id).HasColumnName("id");
        b.Property(x => x.ScientificName).HasColumnName("scientific_name").HasMaxLength(100).IsRequired();
        b.Property(x => x.CommonName).HasColumnName("common_name").HasMaxLength(100).IsRequired();
        b.Property(x => x.Description).HasColumnName("description");
        b.HasIndex(x => x.ScientificName).IsUnique();
    }
}
