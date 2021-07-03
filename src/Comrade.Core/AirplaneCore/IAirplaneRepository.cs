﻿#region

using System.Threading.Tasks;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Domain.Models;

#endregion

namespace Comrade.Core.AirplaneCore
{
    public interface IAirplaneRepository : IRepository<Airplane>
    {
        Task<ISingleResult<Airplane>> RegistroCodeRepetido(int id, string codigo);
    }
}