#region

using Comrade.External.Utils;

#endregion

namespace Comrade.External.Bases
{
    public class EntityDto : Dto, IEntityDto
    {
        public int Id { get; set; }
    }
}