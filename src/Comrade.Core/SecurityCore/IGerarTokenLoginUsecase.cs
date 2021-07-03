#region

using System.Threading.Tasks;
using Comrade.Core.Utils;

#endregion

namespace Comrade.Core.SecurityCore
{
    public interface IGerarTokenLoginUsecase
    {
        Task<SecurityResult> Execute(string chave, string senha);
    }
}