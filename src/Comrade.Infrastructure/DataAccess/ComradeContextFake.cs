#region

using System;
using Comrade.Domain.Models;
using Comrade.Infrastructure.Mappings;
using Microsoft.EntityFrameworkCore;

#endregion

namespace Comrade.Infrastructure.DataAccess
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
        public DbSet<SystemUser> SystemUsers { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tabelas
            modelBuilder.ApplyConfiguration(new AirplaneConfiguration());
            modelBuilder.ApplyConfiguration(new SystemUserConfiguration());
        }
    }
}