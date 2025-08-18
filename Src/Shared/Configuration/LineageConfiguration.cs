using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ColombianCoffee.Modules.Varieties.Domain.Entities;

namespace ColombianCoffee.src.Shared.Configuration;

public class LineageConfiguration : IEntityTypeConfiguration<Lineage>
{
    public void Configure(EntityTypeBuilder<Lineage> b)
    {
        b.ToTable("lineage");
        b.HasKey(x => x.Id);
        b.Property(x => x.Id).HasColumnName("id");
        b.Property(x => x.Name).HasColumnName("name").HasMaxLength(150).IsRequired();
        b.Property(x => x.Description).HasColumnName("description");
        b.Property(x => x.Notes).HasColumnName("notes");
    }
}
