#region

using Comrade.Core.Helpers.Messages;
using comrade.Domain.Bases;
using comrade.Domain.Enums;

#endregion

namespace comrade.Core.Helpers.Models.Results
{
    public class DeleteResult<TEntity> : SingleResult<TEntity>
        where TEntity : Entity
    {
        public DeleteResult()
        {
            Code = (int) EnumResultadoAcao.Success;
            Success = true;
            Message = BusinessMessage.ResourceManager.GetString("MSG03");
        }

        public DeleteResult(bool success, string message)
        {
            Code = success ? (int) EnumResultadoAcao.Success : (int) EnumResultadoAcao.ErroNaoEncontrado;
            Success = success;
            Message = message;
        }

        public DeleteResult(string message)
        {
            Code = (int) EnumResultadoAcao.ErroNaoEncontrado;
            Success = false;
            Message = message;
        }
    }
}