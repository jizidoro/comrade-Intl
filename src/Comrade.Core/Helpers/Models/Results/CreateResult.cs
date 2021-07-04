﻿#region

using System;
using Comrade.Core.Helpers.Messages;
using Comrade.Domain.Bases;
using Comrade.Domain.Enums;

#endregion

namespace Comrade.Core.Helpers.Models.Results
{
    public class CreateResult<TEntity> : SingleResult<TEntity>
        where TEntity : Entity
    {
        public CreateResult()
        {
            Code = (int) EnumResultadoAcao.Success;
            Success = true;
            Message = BusinessMessage.ResourceManager.GetString("MSG01");
        }

        public CreateResult(bool success, string message)
        {
            Code = success ? (int) EnumResultadoAcao.Success : (int) EnumResultadoAcao.ErroNaoEncontrado;
            Success = success;
            Message = message;
        }

        public CreateResult(Exception ex)
        {
            Code = (int)EnumResultadoAcao.ErroServidor;
            Success = false;
            ExceptionMessage = ex.Message;
            Message = BusinessMessage.ResourceManager.GetString("MSG07");
        }
    }
}