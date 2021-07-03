#region

using comrade.Application.Bases;

#endregion

namespace comrade.Application.Dtos
{
    public class UserDto : EntityDto
    {
        public string Token { get; set; }
    }
}