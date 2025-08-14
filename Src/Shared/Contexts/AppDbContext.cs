using Microsoft.EntityFrameworkCore;
using ColombianCoffee.src.Modules.Varieties.Domain.Entities;


namespace ColombianCoffee.Src.Shared.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Variety> Varieties { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<GeneticGroup> GeneticGroup { get; set; }
        public DbSet<Lineage> Lineage { get; set; }
        public DbSet<AltitudeQuality> AltitudeQuality { get; set; }
        public DbSet<MeasurementUnit> MeasurementUnit { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}