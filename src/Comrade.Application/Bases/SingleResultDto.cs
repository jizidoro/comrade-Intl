#region

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Comrade.Application.Utils;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Messages;
using Comrade.Core.Utils;
using Comrade.Domain.Bases;
using Comrade.Domain.Enums;

#endregion

namespace Comrade.Application.Bases
{
    public class SingleResultDto<TDto> : ResultDto, ISingleResultDto<TDto>
        where TDto : Dto
    {
        public SingleResultDto(TDto data)
        {
            Code = data == null ? (int) EnumResultadoAcao.ErroNaoEncontrado : (int) EnumResultadoAcao.Success;
            Success = data != null;
            Message = data == null ? BusinessMessage.ResourceManager.GetString("MSG04") : string.Empty;
            Data = data;
        }

        public SingleResultDto()
        {
            Code = (int) EnumResultadoAcao.ErroNaoEncontrado;
            Success = false;
            Message = BusinessMessage.ResourceManager.GetString("MSG04");
            Data = null;
        }

        public SingleResultDto(SecurityResult erroSecurity)
        {
            Code = erroSecurity.Code;
            Success = false;
            Message = erroSecurity.ErrorMessage;
            Data = null;
        }


        public SingleResultDto(Exception ex)
        {
            Code = (int) EnumResultadoAcao.ErroServidor;
            Success = false;
            Message = ex.Message;
        }

        public SingleResultDto(IEnumerable<string> listErrors)
        {
            Code = (int) EnumResultadoAcao.ErroValidacaoNegocio;
            Success = false;
            Mensagens = listErrors.ToList();
        }

        public SingleResultDto(IResult result)
        {
            Code = result.Code;
            Success = result.Success;
            Message = result.Message;
        }

        public SingleResultDto(int code, bool success, string message)
        {
            Code = code;
            Success = success;
            Message = message;
        }

        public TDto Data { get; private set; }

        public void SetData<TEntity>(ISingleResult<TEntity> result, IMapper mapper)
            where TEntity : Entity
        {
            Data = mapper.Map<TDto>(result.Data);
        }
    }
}