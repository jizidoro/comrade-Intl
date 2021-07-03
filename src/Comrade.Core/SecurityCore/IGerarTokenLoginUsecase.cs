#region

using System.Threading.Tasks;
using comrade.Core.Utils;

#endregion

namespace comrade.Core.SecurityCore
{
    public interface IGenerateTokenLoginUseCase
    {
        Task<SecurityResult> Execute(string key, string password);
    }
}