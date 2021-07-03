#region

using Comrade.Domain.Models.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

#endregion

namespace Comrade.Infrastructure.Mappings.Views
{
    public class VwUserSystemPermissionConfiguration : IEntityTypeConfiguration<VwUserSystemPermission>
    {
        public void Configure(EntityTypeBuilder<VwUserSystemPermission> builder)
        {
            builder.ToView("VW_USSP_USUARIO_SISTEMA_PERMISSAO").HasNoKey();

            builder.Property(b => b.SqUserSystem).HasColumnName("USSI_SQ_USUARIO_SISTEMA");
        }
    }
}