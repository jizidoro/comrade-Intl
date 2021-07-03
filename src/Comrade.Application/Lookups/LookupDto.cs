#region

using Comrade.Application.Bases;
using Comrade.Application.Utils;

#endregion

namespace Comrade.Application.Lookups
{
    public class LookupDto : EntityDto, ILookupDto
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}