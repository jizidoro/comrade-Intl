#region

using System.Threading.Tasks;
using Comrade.Core.Utils;

#endregion

namespace Comrade.Core.SecurityCore
{
    public interface IGenerateTokenUseCase
    {
        string Execute(TokenUser tokenUser);
    }
}