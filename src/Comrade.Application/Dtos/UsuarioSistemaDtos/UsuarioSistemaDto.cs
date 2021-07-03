#region

using System;
using Comrade.Application.Bases;

#endregion

namespace Comrade.Application.Dtos.UsuarioSistemaDtos
{
    public class UsuarioSistemaDto : EntityDto
    {
        public int? Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Situacao { get; set; }
        public string Matricula { get; set; }
        public DateTime DataRegistro { get; set; }
    }
}