#region

using System.Threading.Tasks;
using Comrade.Core.Utils;

#endregion

namespace Comrade.Core.SecurityCore
{
    public interface IValidateLoginUseCase
    {
        Task<SecurityResult> Execute(string key, string password);
    }
}