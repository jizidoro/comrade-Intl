#region

using System.Collections.Generic;
using Comrade.External.Utils;

#endregion

namespace Comrade.External.Bases
{
    public class ResultDto
    {
        public int Codigo { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public IList<string> Mensagens { get; set; }
    }
}