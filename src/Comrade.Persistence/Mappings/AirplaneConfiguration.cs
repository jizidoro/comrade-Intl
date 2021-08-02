﻿#region

using Comrade.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace Comrade.Persistence.Mappings
{
    public class AirplaneConfiguration : IEntityTypeConfiguration<Airplane>
    {
        public void Configure(EntityTypeBuilder<Airplane> builder)
        {
            builder.Property(b => b.Id).HasColumnName("AIRP_SQ_AIRPLANE").IsRequired();
            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.Code).HasDatabaseName("IX_UN_AIRP_TX_CODIGO").IsUnique();
        }
    }
}