#region

using System;
using Comrade.Application.Bases;

#endregion

namespace Comrade.Application.Dtos.SystemUserDtos
{
    public class SystemUserDto : EntityDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool Situacao { get; set; }
        public string? Registration { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}