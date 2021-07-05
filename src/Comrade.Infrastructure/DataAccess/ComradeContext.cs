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
        public ComradeContext(DbContextOptions<ComradeContext> options)
            : base(options)
        {
        }

        // Tables
        public DbSet<Airplane>? Airplanes { get; set; }
        public DbSet<SystemUser>? SystemUsers { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Tables
            modelBuilder.ApplyConfiguration(new AirplaneConfiguration());
            modelBuilder.ApplyConfiguration(new SystemUserConfiguration());
        }
    }
}