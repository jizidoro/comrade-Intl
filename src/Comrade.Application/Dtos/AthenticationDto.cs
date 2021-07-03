#region

using comrade.Application.Bases;

#endregion

namespace comrade.Application.Dtos
{
    public class AthenticationDto : EntityDto
    {
        public string Key { get; set; }
        public string Password { get; set; }
    }
}