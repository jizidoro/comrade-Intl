#region

using comrade.Domain.Models;
using comrade.Domain.Models.Views;
using comrade.Infrastructure.Mappings;
using comrade.Infrastructure.Mappings.Views;
using Microsoft.EntityFrameworkCore;

#endregion

namespace comrade.Infrastructure.DataAccess
{
    public class ComradeContext : DbContext
    {
        private const string JsonPath = "Comrade.Infrastructure.SeedData";

        public ComradeContext(DbContextOptions<ComradeContext> options)
            : base(options)
        {
        }

        // Tabelas
        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<UserSystem> UserSystems { get; set; }

        // Views
        public DbSet<VwUserSystemPermission> VUserSystemPermissoes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tabelas
            modelBuilder.ApplyConfiguration(new AirplaneConfiguration());
            modelBuilder.ApplyConfiguration(new UserSystemConfiguration());

            // Views
            modelBuilder.ApplyConfiguration(new VwUserSystemPermissionConfiguration());
        }
    }
}