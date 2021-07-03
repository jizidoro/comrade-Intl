#region

using System.Linq;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Domain.Bases;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.UsuarioSistemaCore
{
    public interface IUsuarioSistemaRepository : IRepository<UsuarioSistema>
    {
        IQueryable<LookupEntity> BuscarPorNome(string nome);
    }
}