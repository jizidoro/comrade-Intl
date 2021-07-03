#region

using Comrade.Core.Helpers.Messages;
using comrade.Domain.Bases;
using comrade.Domain.Enums;

#endregion

namespace comrade.Core.Helpers.Models.Results
{
    public class CreateResult<TEntity> : SingleResult<TEntity>
        where TEntity : Entity
    {
        public CreateResult(TEntity data)
        {
            Code = (int) EnumResultadoAcao.Success;
            Success = true;
            Message = BusinessMessage.ResourceManager.GetString("MSG01");
            Data = data;
        }

        public CreateResult(bool success, string message)
        {
            Code = success ? (int) EnumResultadoAcao.Success : (int) EnumResultadoAcao.ErroNaoEncontrado;
            Success = success;
            Message = message;
        }

        public CreateResult(string message)
        {
            Code = (int) EnumResultadoAcao.ErroNaoEncontrado;
            Success = false;
            Message = message;
        }
    }
}