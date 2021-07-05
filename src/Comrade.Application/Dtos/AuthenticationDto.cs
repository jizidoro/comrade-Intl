#region

using Comrade.Application.Bases;

#endregion

namespace Comrade.Application.Dtos
{
    public class AuthenticationDto : EntityDto
    {
        public AuthenticationDto()
        {
            Key = "";
            Password = "";
        }

        public string Key { get; set; }
        public string Password { get; set; }
    }
}