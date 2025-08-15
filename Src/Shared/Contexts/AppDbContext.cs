using Microsoft.EntityFrameworkCore;
using ColombianCoffee.Src.Modules.Auth.Domain.Entities;
using System.Linq;

namespace ColombianCoffee.Src.Shared.Contexts
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

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
                    .HasColumnName("Name") // 🔹 Mapea Username en C# a Name en MySQL
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

                // Índice único para email
                entity.HasIndex(u => u.Email)
                    .IsUnique();

                // Opcional: índice único para username
                entity.HasIndex(u => u.Username)
                    .IsUnique();
            });

            // 🔹 Convención global: todos los nombres de tablas en minúsculas
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
