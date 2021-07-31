#region

using System.Threading.Tasks;
using Comrade.Core.Bases.Interfaces;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.AirplaneCore
{
    public interface IAirplaneDeleteUseCase
    {
        Task<ISingleResult<Airplane>> Execute(int id);
    }
}