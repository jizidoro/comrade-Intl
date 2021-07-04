#region

using System.Collections.Generic;
using Comrade.External.Bases;

#endregion

namespace Comrade.External.Utils
{
    public interface IListResultDto<T>
    {
        IList<T> Data { get; set; }
    }
}