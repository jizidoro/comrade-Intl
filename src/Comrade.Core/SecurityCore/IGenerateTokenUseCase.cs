#region

using Comrade.Domain.Token;

#endregion

namespace Comrade.Core.SecurityCore
{
    public interface IGenerateTokenUseCase
    {
        string Execute(TokenUser tokenUser);
    }
}