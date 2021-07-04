#region

using System.Collections.Generic;
using Comrade.Application.BaseInterfaces;
using Comrade.Core.Helpers.Messages;
using Comrade.Domain.Enums;

#endregion

namespace Comrade.Application.Bases
{
    public class ListResultDto<T> : ResultDto, IListResultDto<T>
        where T : Dto
    {
        public ListResultDto()
        {
        }

        public ListResultDto(IList<T> data)
        {
            Data = data;
            Code = data == null ? (int) EnumResultadoAcao.ErroNaoEncontrado : (int) EnumResultadoAcao.Success;
            Success = data != null;
            Message = data == null ? BusinessMessage.ResourceManager.GetString("MSG04") : string.Empty;
        }

        public ListResultDto(int code, string menssagem)
        {
            Code = code;
            Success = false;
            Message = menssagem;
        }

        public ListResultDto(string menssagem)
        {
            Code = (int) EnumResultadoAcao.ErroValidacaoNegocio;
            Success = false;
            Message = menssagem;
        }

        public IList<T> Data { get; set; }
    }
}