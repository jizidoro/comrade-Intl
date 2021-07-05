#region

using System;
using System.Globalization;
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
            Code = (int) EnumResponse.Success;
            Success = true;
            Message = BusinessMessage.ResourceManager.GetString("MSG01",CultureInfo.CurrentCulture);
        }

        public CreateResult(bool success, string? message)
        {
            Code = success ? (int) EnumResponse.Success : (int) EnumResponse.ErrorNotFound;
            Success = success;
            Message = message;
        }

        public CreateResult(Exception ex)
        {
            Code = (int)EnumResponse.ErrorServer;
            Success = false;
            ExceptionMessage = ex.Message;
            Message = BusinessMessage.ResourceManager.GetString("MSG07", CultureInfo.CurrentCulture);
        }
    }
}