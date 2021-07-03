#region

using Comrade.Application.Utils;

#endregion

namespace Comrade.Application.Bases
{
    public class LookupDto : EntityDto, ILookupDto
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}