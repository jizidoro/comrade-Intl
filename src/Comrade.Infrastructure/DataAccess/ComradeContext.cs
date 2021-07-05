#region

using Comrade.Domain.Models;
using Comrade.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Comrade.Infrastructure.DataAccess
{
    public class ComradeContext : DbContext
    {
#pragma warning disable CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
        public ComradeContext(DbContextOptions<ComradeContext> options)
#pragma warning restore CS8618 // Non-nullable field is uninitialized. Consider declaring as nullable.
            : base(options)
        {
        }

        // Tables
        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<SystemUser> SystemUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tables
            modelBuilder.ApplyConfiguration(new AirplaneConfiguration());
            modelBuilder.ApplyConfiguration(new SystemUserConfiguration());
        }
    }
}