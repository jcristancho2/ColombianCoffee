using ColombianCoffee.Modules.Varieties.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ColombianCoffee.src.Shared.Configuration;

public class VarietyConfiguration : IEntityTypeConfiguration<Variety>
{
    public void Configure(EntityTypeBuilder<Variety> b)
    {
        b.ToTable("varieties");

        b.HasKey(v => v.Id);
        b.Property(v => v.Id)
            .HasColumnName("id")
            .IsRequired();

        // Identidad
        b.Property(v => v.Name)
            .HasColumnName("name")
            .HasMaxLength(200)
            .IsRequired();

        b.Property(v => v.ScientificName)
            .HasColumnName("scientific_name")
            .HasMaxLength(200)
            .IsRequired();

        b.Property(v => v.History)
            .HasColumnName("history");

        // Agronómicos / categóricos (la DB usa ENUM; aquí los manejamos como string)
        b.Property(v => v.PlantHeight)
            .HasColumnName("plant_height")
            .HasMaxLength(20)
            .IsRequired();

        b.Property(v => v.BeanSize)
            .HasColumnName("bean_size")
            .HasMaxLength(20)
            .IsRequired();

        b.Property(v => v.YieldPotential)
            .HasColumnName("yield_potential")
            .HasMaxLength(20)
            .IsRequired();

        b.Property(v => v.RustResistance)
            .HasColumnName("rust_resistance")
            .HasMaxLength(30)
            .IsRequired();

        b.Property(v => v.AnthracnoseResistance)
            .HasColumnName("anthracnose_resistance")
            .HasMaxLength(30)
            .IsRequired();

        b.Property(v => v.NematodesResistance)
            .HasColumnName("nematodes_resistance")
            .HasMaxLength(30)
            .IsRequired();

        b.Property(v => v.NutritionalRequirement)
            .HasColumnName("nutritional_requirement")
            .HasMaxLength(20);

        b.Property(v => v.HarvestTime)
            .HasColumnName("harvest_time")
            .HasMaxLength(20);

        b.Property(v => v.MaturationTime)
            .HasColumnName("maturation_time")
            .HasMaxLength(20);

        b.Property(v => v.MinAltitude)
            .HasColumnName("min_altitude")
            .IsRequired();

        b.Property(v => v.MaxAltitude)
            .HasColumnName("max_altitude")
            .IsRequired();

        b.Property(v => v.PlantingDensityValue)
            .HasColumnName("planting_density_value")
            .HasPrecision(8, 2);

        b.Property(v => v.ImageUrl)
            .HasColumnName("image_url")
            .HasMaxLength(500);

        // Relaciones (FKs) — tipos alineados a tu esquema
        b.Property(v => v.SpeciesId).HasColumnName("species_id");
        b.Property(v => v.GeneticGroupId).HasColumnName("genetic_group_id");
        b.Property(v => v.LineageId).HasColumnName("lineage_id");
        b.Property(v => v.AltitudeQualityId).HasColumnName("altitude_quality_id");

        // Unidades de medida
        b.Property(v => v.PlantingDensityUnitId).HasColumnName("planting_density_unit_id");
        b.Property(v => v.AltitudeUnitId).HasColumnName("altitude_unit_id");

        b.HasOne(v => v.Species)
            .WithMany()
            .HasForeignKey(v => v.SpeciesId)
            .OnDelete(DeleteBehavior.Cascade);

        b.HasOne(v => v.GeneticGroup)
            .WithMany()
            .HasForeignKey(v => v.GeneticGroupId)
            .OnDelete(DeleteBehavior.Cascade);

        b.HasOne(v => v.Lineage)
            .WithMany()
            .HasForeignKey(v => v.LineageId)
            .OnDelete(DeleteBehavior.Cascade);

        b.HasOne(v => v.AltitudeQuality)
            .WithMany()
            .HasForeignKey(v => v.AltitudeQualityId)
            .OnDelete(DeleteBehavior.Cascade);
            
        // Relaciones para unidades de medida
        b.HasOne(v => v.PlantingDensityUnit)
            .WithMany()
            .HasForeignKey(v => v.PlantingDensityUnitId)
            .OnDelete(DeleteBehavior.SetNull);
            
        b.HasOne(v => v.AltitudeUnit)
            .WithMany()
            .HasForeignKey(v => v.AltitudeUnitId)
            .OnDelete(DeleteBehavior.SetNull);

        // Índices (alineados a tu SQL para que EF también los conozca)
        b.HasIndex(v => v.Name).HasDatabaseName("idx_varieties_name");
        b.HasIndex(v => v.PlantHeight).HasDatabaseName("idx_varieties_plant_height");
        b.HasIndex(v => v.BeanSize).HasDatabaseName("idx_varieties_bean_size");
        b.HasIndex(v => v.YieldPotential).HasDatabaseName("idx_varieties_yield_potential");
        b.HasIndex(v => v.RustResistance).HasDatabaseName("idx_varieties_rust_resistance");
        b.HasIndex(v => v.AnthracnoseResistance).HasDatabaseName("idx_varieties_anthracnose_resistance");
        b.HasIndex(v => v.NematodesResistance).HasDatabaseName("idx_varieties_nematodes_resistance");
        b.HasIndex(v => v.MinAltitude).HasDatabaseName("idx_varieties_min_altitude");
        b.HasIndex(v => v.MaxAltitude).HasDatabaseName("idx_varieties_max_altitude");
        b.HasIndex(v => v.AltitudeQualityId).HasDatabaseName("idx_varieties_altitude_quality");
        b.HasIndex(v => v.SpeciesId).HasDatabaseName("idx_varieties_species_id");
        b.HasIndex(v => v.GeneticGroupId).HasDatabaseName("idx_varieties_genetic_group_id");
        b.HasIndex(v => v.LineageId).HasDatabaseName("idx_varieties_lineage_id");
        b.HasIndex(v => v.PlantingDensityUnitId).HasDatabaseName("idx_varieties_planting_density_unit");
        b.HasIndex(v => v.AltitudeUnitId).HasDatabaseName("idx_varieties_altitude_unit");
    }
}
