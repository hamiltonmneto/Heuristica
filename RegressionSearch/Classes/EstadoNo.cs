using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Heuristica.Classes
{
    public class EstadoNo
    {
        public string[,] estadoAtual { get; set; }
        public int heuristica { get; set; }
        public List<EstadoNo> estadosFilhos { get; set; }
    }
}
