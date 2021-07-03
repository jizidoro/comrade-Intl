#region

using Comrade.Application.Bases;

#endregion

namespace Comrade.Application.Dtos
{
    public class AutenticacaoDto : EntityDto
    {
        public string Chave { get; set; }
        public string Senha { get; set; }
    }
}