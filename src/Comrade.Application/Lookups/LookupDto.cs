#region

using comrade.Application.Bases;
using comrade.Application.Utils;

#endregion

namespace comrade.Application.Lookups
{
    public class LookupDto : EntityDto, ILookupDto
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}