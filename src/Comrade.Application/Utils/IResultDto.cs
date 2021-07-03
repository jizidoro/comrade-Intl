#region

using System.Collections.Generic;
using Comrade.Core.Helpers.Interfaces;

#endregion

namespace Comrade.Application.Utils
{
    public interface IResultDto : IResult
    {
        IList<string> Mensagens { get; set; }
    }
}