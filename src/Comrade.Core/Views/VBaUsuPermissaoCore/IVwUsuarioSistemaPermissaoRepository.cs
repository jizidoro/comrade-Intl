#region

using System.Linq;
using Comrade.Domain.Models.Views;

#endregion

namespace Comrade.Core.Views.VBaUsuPermissaoCore
{
    public interface IVwUsuarioSistemaPermissaoRepository
    {
        IQueryable<VwUsuarioSistemaPermissao> ListarPorSqUsuarioSistema(int sqUsuarioSistema);
    }
}