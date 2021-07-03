#region

using System;
using System.Collections.Generic;
using comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Messages;
using comrade.Domain.Enums;
using comrade.Domain.Interfaces;

#endregion

namespace comrade.Core.Helpers.Models.Results
{
    public class SingleResult<TEntity> : ISingleResult<TEntity>
        where TEntity : IEntity
    {
        public SingleResult()
        {
            Code = (int) EnumResultadoAcao.Success;
            Success = true;
        }

        public SingleResult(string message)
        {
            Code = (int) EnumResultadoAcao.ErroValidacaoNegocio;
            Success = false;
            Message = message;
        }

        public SingleResult(IEnumerable<string> mensagens)
        {
            Code = (int) EnumResultadoAcao.ErroValidacaoNegocio;
            Success = false;
            Mensagens = mensagens;
        }


        public SingleResult(int codigo, string message)
        {
            Code = codigo;
            Success = false;
            Message = message;
        }

        public SingleResult(Exception ex)
        {
            Code = (int) EnumResultadoAcao.ErroServidor;
            Success = false;
            Message = BusinessMessage.ResourceManager.GetString("MSG07");
        }

        public SingleResult(TEntity data)
        {
            Code = data == null ? (int) EnumResultadoAcao.ErroNaoEncontrado : (int) EnumResultadoAcao.Success;
            Success = data != null;
            Message = data == null ? BusinessMessage.ResourceManager.GetString("MSG04") : string.Empty;
            Data = data;
        }

        public IEnumerable<string> Mensagens { get; set; }

        public int Code { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public TEntity Data { get; set; }
    }
}