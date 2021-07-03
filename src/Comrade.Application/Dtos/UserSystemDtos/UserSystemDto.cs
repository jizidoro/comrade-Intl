#region

using System;
using comrade.Application.Bases;

#endregion

namespace comrade.Application.Dtos.UserSystemDtos
{
    public class UserSystemDto : EntityDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Situacao { get; set; }
        public string Registration { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}