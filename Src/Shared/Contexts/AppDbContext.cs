using System;
using System.Collections.Generic;
using System.Linq;
<<<<<<< HEAD
using System.Threading.Tasks;
=======
using Microsoft.EntityFrameworkCore;
using ColombianCoffee.Src.Modules.Auth.Domain.Entities;
using ColombianCoffee.Src.Modules.Varieties.Domain.Entities;
>>>>>>> release/v1.0.0

using Microsoft.EntityFrameworkCore;

//asignacion de las entidades

namespace ColombianCoffee.Src.Shared.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Define DbSets for your entities here
        // public DbSet<YourEntity> YourEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}