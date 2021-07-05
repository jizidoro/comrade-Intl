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

        public ListResultDto(IList<T> data)
        {
            Data = data;
            Code = data == null ? (int) EnumResponse.ErrorNotFound : (int) EnumResponse.Success;
            Success = data != null;
            Message = data == null ? BusinessMessage.ResourceManager.GetString("MSG04") : string.Empty;
        }

        public ListResultDto(int code, string message)
        {
            Code = code;
            Success = false;
            Message = message;
        }

        public ListResultDto(string message)
        {
            Code = (int) EnumResponse.ErrorBusinessValidation;
            Success = false;
            Message = message;
        }

        public IList<T>? Data { get; set; }
    }
}