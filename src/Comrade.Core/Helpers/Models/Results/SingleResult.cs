#region

using System;
using System.Collections.Generic;
using Comrade.Core.Helpers.Interfaces;
using Comrade.Core.Helpers.Messages;
using Comrade.Domain.Enums;
using Comrade.Domain.Interfaces;

#endregion

namespace Comrade.Core.Helpers.Models.Results
{
    public class SingleResult<TEntity> : ISingleResult<TEntity>
        where TEntity : IEntity
    {
        public SingleResult()
        {
            Codigo = (int) EnumResultadoAcao.Sucesso;
            Sucesso = true;
        }

        public SingleResult(string mensagem)
        {
            Codigo = (int) EnumResultadoAcao.ErroValidacaoNegocio;
            Sucesso = false;
            Mensagem = mensagem;
        }

        public SingleResult(IEnumerable<string> mensagens)
        {
            Codigo = (int) EnumResultadoAcao.ErroValidacaoNegocio;
            Sucesso = false;
            Mensagens = mensagens;
        }


        public SingleResult(int codigo, string mensagem)
        {
            Codigo = codigo;
            Sucesso = false;
            Mensagem = mensagem;
        }

        public SingleResult(Exception ex)
        {
            Codigo = (int) EnumResultadoAcao.ErroServidor;
            Sucesso = false;
            Mensagem = MensagensNegocio.ResourceManager.GetString("MSG07");
        }

        public SingleResult(TEntity data)
        {
            Codigo = data == null ? (int) EnumResultadoAcao.ErroNaoEncontrado : (int) EnumResultadoAcao.Sucesso;
            Sucesso = data != null;
            Mensagem = data == null ? MensagensNegocio.ResourceManager.GetString("MSG04") : string.Empty;
            Data = data;
        }

        public IEnumerable<string> Mensagens { get; set; }

        public int Codigo { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public TEntity Data { get; set; }
    }
}