#region

using System.Collections.Generic;
using Comrade.Application.BaseInterfaces;

#endregion

namespace Comrade.Application.Bases
{
    public class ResultDto : IResultDto
    {
        public int Code { get; set; }
        public bool Success { get; set; }
        public string? ExceptionMessage { get; set; }
        public string? Message { get; set; } = "";
        public IList<string>? Messages { get; set; }
    }
}