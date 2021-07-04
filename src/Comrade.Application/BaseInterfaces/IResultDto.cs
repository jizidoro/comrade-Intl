#region

using System.Collections.Generic;
using Comrade.Core.Helpers.Interfaces;

#endregion

namespace Comrade.Application.BaseInterfaces
{
    public interface IResultDto : IResult
    {
        IList<string> Messages { get; set; }
    }
}