using System.Linq;
using Microsoft.EntityFrameworkCore;
using ColombianCoffee.Src.Modules.Auth.Domain.Entities;
using ColombianCoffee.src.Modules.Varieties.Domain.Entities;
using ColombianCoffee.Modules.Varieties.Domain.Entities;


namespace ColombianCoffee.Src.Shared.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Variety> Varieties { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<GeneticGroup> GeneticGroup { get; set; }
        public DbSet<Lineage> Lineage { get; set; }
        public DbSet<AltitudeQuality> AltitudeQuality { get; set; }
        public DbSet<MeasurementUnit> MeasurementUnit { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Aplicar configuraciones desde el ensamblado
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            // Configuración explícita de User
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");

                entity.HasKey(u => u.Id);

                entity.Property(u => u.Username)
                    .HasColumnName("Name")
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(u => u.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(u => u.Role)
                    .IsRequired();

                entity.HasIndex(u => u.Email)
                    .IsUnique();

                entity.HasIndex(u => u.Username)
                    .IsUnique();
            });

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entity.GetTableName();
                if (!string.IsNullOrEmpty(tableName))
                {
                    entity.SetTableName(tableName.ToLower());
                }
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}