#region

using Comrade.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace Comrade.Persistence.Mappings
{
    public class SystemUserConfiguration : IEntityTypeConfiguration<SystemUser>
    {
        public void Configure(EntityTypeBuilder<SystemUser> builder)
        {
            builder.Property(b => b.Id).HasColumnName("USSI_SQ_USUARIO_SISTEMA").IsRequired();
            builder.HasKey(c => c.Id);

            builder.HasIndex(c => c.Email).HasDatabaseName("IX_UN_USSI_TX_EMAIL").IsUnique();
            builder.HasIndex(c => c.Registration).HasDatabaseName("IX_UN_USSI_TX_MATRICULA")
                .IsUnique();
        }
    }
}