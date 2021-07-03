#region

using System.Collections.Generic;
using comrade.Application.Utils;

#endregion

namespace comrade.Application.Bases
{
    public class ResultDto : IResultDto
    {
        public int Code { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public IList<string> Mensagens { get; set; }
    }
}