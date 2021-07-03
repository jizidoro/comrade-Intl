#region

using Comrade.Application.Bases;

#endregion

namespace Comrade.Application.Dtos
{
    public class UsuarioDto : EntityDto
    {
        public string Token { get; set; }
    }
}