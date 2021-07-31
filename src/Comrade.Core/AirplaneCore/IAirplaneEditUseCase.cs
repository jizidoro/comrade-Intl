#region

using System.Threading.Tasks;
using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.AirplaneCore
{
    public interface IAirplaneEditUseCase
    {
        Task<ISingleResult<Airplane>> Execute(Airplane entity);
    }
}