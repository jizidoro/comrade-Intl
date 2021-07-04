#region

using System.Collections.Generic;

#endregion

namespace Comrade.Core.Utils
{
    public class TokenUser
    {
        public string Key { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public IList<string> Roles { get; set; }
    }
}