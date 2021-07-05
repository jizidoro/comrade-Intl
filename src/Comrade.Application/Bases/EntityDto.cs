#region

using Comrade.Application.BaseInterfaces;

#endregion

namespace Comrade.Application.Bases
{
    public class EntityDto : Dto, IEntityDto
    {
        public int Id { get; set; }
    }
}