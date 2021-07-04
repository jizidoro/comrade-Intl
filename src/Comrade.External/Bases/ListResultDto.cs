#region

using System.Collections.Generic;
using Comrade.Domain.Enums;
using Comrade.External.Utils;

#endregion

namespace Comrade.External.Bases
{
    public class ListResultDto<T> : ResultDto, IListResultDto<T>
    {
        public ListResultDto()
        {
        }

        public ListResultDto(IList<T> data)
        {
            Data = data;
            Codigo = data == null ? (int) EnumResultadoAcao.ErroNaoEncontrado : (int) EnumResultadoAcao.Sucesso;
            Sucesso = data != null;
        }

        public IList<T> Data { get; set; }
    }
}