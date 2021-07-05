#region

using Comrade.Application.Bases;

#endregion

namespace Comrade.Application.Dtos
{
    public class UserDto : EntityDto
    {
        public string? Token { get; set; }
    }
}