#region

using Comrade.Domain.Interfaces;

#endregion

namespace Comrade.Domain.Bases
{
    public class LookupEntity : ILookupEntity
    {
        public int Key { get; set; }
        public string Value { get; set; }
    }
}