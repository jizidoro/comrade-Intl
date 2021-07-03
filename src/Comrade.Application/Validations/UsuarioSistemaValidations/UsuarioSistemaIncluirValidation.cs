#region

using Comrade.Application.Dtos.UsuarioSistemaDtos;

#endregion

namespace Comrade.Application.Validations.UsuarioSistemaValidations
{
    public class UsuarioSistemaIncluirValidation : UsuarioSistemaValidation<UsuarioSistemaIncluirDto>
    {
        public UsuarioSistemaIncluirValidation()
        {
            ValidarNome();
            ValidarEmail();
            ValidarSenha();
            ValidarMatricula();
        }
    }
}