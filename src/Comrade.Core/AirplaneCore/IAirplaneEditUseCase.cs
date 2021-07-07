#region

using System.Threading.Tasks;
using Comrade.Core.Helpers.Models.Interfaces;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.AirplaneCore
{
    public interface IAirplaneEditUseCase
    {
        Task<ISingleResult<Airplane>> Execute(Airplane entity);
    }
}