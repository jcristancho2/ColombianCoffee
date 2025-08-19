using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ColombianCoffee.Src.Modules.Varieties.Domain.Entities;

namespace ColombianCoffee.Src.Shared.Configuration;

public class GeneticGroupConfiguration : IEntityTypeConfiguration<GeneticGroup>
{
    public void Configure(EntityTypeBuilder<GeneticGroup> b)
    {
        b.ToTable("genetic_group");
        b.HasKey(x => x.Id);
        b.Property(x => x.Id).HasColumnName("id");
        b.Property(x => x.Code).HasColumnName("code").HasMaxLength(50).IsRequired();
        b.Property(x => x.Name).HasColumnName("name").HasMaxLength(150).IsRequired();
        b.Property(x => x.Notes).HasColumnName("notes");
    }
}
