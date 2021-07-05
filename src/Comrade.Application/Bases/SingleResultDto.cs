#region

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AutoMapper;
using Comrade.Application.BaseInterfaces;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Messages;
using Comrade.Core.Utils;
using Comrade.Domain.Bases;
using Comrade.Domain.Enums;
using FluentValidation.Results;

#endregion

namespace Comrade.Application.Bases
{
    public class SingleResultDto<TDto> : ResultDto, ISingleResultDto<TDto>
        where TDto : Dto
    {
        public SingleResultDto(TDto data)
        {
            Code = data == null ? (int) EnumResponse.ErrorNotFound : (int) EnumResponse.Success;
            Success = data != null;
            Message = data == null ? BusinessMessage.ResourceManager.GetString("MSG04", CultureInfo.CurrentCulture) : string.Empty;
            Data = data;
        }

        public SingleResultDto()
        {
            Code = (int) EnumResponse.ErrorBusinessValidation;
            Success = false;
            Message = BusinessMessage.ResourceManager.GetString("MSG04", CultureInfo.CurrentCulture);
        }

        public SingleResultDto(ValidationResult validationResult)
        {
            Code = (int) EnumResponse.ErrorBusinessValidation;
            Success = false;
            Messages = validationResult.Errors.Select(x => x.ErrorMessage).ToList();
            ValidationResult = validationResult;
        }

        public SingleResultDto(SecurityResult errorSecurity)
        {
            Code = errorSecurity.Code;
            Success = false;
            Message = errorSecurity.ErrorMessage;
        }


        public SingleResultDto(Exception ex)
        {
            Code = (int) EnumResponse.ErrorServer;
            Success = false;
            Message = ex.Message;
            ExceptionMessage = ex.Message;
        }

        public SingleResultDto(IEnumerable<string> listErrors)
        {
            Code = (int) EnumResponse.ErrorBusinessValidation;
            Success = false;
            Messages = listErrors.ToList();
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

        public TDto? Data { get; private set; }
        public ValidationResult? ValidationResult { get; private set; }

        public void SetData<TEntity>(ISingleResult<TEntity> result, IMapper mapper)
            where TEntity : Entity
        {
            Data = mapper.Map<TDto>(result.Data);
        }
    }
}