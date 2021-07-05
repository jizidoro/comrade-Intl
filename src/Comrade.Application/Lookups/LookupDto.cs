#region

using Comrade.Application.BaseInterfaces;
using Comrade.Application.Bases;

#endregion

namespace Comrade.Application.Lookups
{
    public class LookupDto : EntityDto, ILookupDto
    {
        public int Key { get; set; }
        public string Value { get; set; } = "";
    }
}