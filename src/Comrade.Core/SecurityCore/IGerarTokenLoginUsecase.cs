#region

using System.Threading.Tasks;
using Comrade.Core.Utils;

#endregion

namespace Comrade.Core.SecurityCore
{
    public interface IGenerateTokenLoginUseCase
    {
        Task<SecurityResult> Execute(string key, string password);
    }
}