using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegressionSearch.Classes
{
    public class SlidingGame
    {
        //public string[,] estadofinal = { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", " " } };

        public string[,] estadoFinalTeste = { { "1","2" ,"3" }, {"8"," ","4" }, {"7","6","5" } };
        public string[,] estadoInicialTeste = { { " ","1","2" }, {"7","8","3" }, {"6","5","4" } };


        public List<string> NewState(List<string> estadoInicial)
        {
            var estadoInicialPreparado = PreparaEstadoInicial(estadoInicial);
            estadoInicialPreparado = estadoInicialTeste;
            var novoEstado = CalcularHeuristica(estadoInicialPreparado);
            return estadoInicial;
        }

        private string[,] PreparaEstadoInicial(List<string> estadoinicial)
        {
            var z = 0;
            string[,] estadoInicialPreparado = new string[3, 3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    estadoInicialPreparado[i, j] = estadoinicial[z];
                    z++;
                }
            }

            return estadoInicialPreparado;
        }

        public int CalcularHeuristica(string[,] estadoinicial)
        {
            var heuristica = 0;
            var numeroDeEstados = new Tuple<int, int>(0, 0);

            for(int i = 1; i <= estadoinicial.Length + 1; i++)
            {
                var posicaoElementoEstadoInicial = CoordinatesOf(estadoinicial,i.ToString());
                var posicaoElementoEstadoFinal = CoordinatesOf(estadoFinalTeste, i.ToString());
                heuristica += CalculaHeuristica(posicaoElementoEstadoInicial, posicaoElementoEstadoFinal);
            }

            numeroDeEstados = ChecarNumeroDeEstados(estadoinicial);

            return 0;
        }

        private int CalculaHeuristica(Tuple<int, int> posicaoElementoEstadoInicial, Tuple<int, int> posicaoElementoEstadoFinal)
        {
            var total = 0;
            var posInicialLinha = posicaoElementoEstadoInicial.Item1;
            var posFinalLinha = posicaoElementoEstadoFinal.Item1;

            var posInicialColuna = posicaoElementoEstadoInicial.Item2;
            var posFinalColuna = posicaoElementoEstadoFinal.Item2;

            while (posInicialLinha != posFinalLinha)
            {
                if (posInicialLinha > posFinalLinha)
                    posInicialLinha--;
                else
                    posInicialLinha++;
                total++;
            }

            while(posInicialColuna != posFinalColuna)
            {
                if (posInicialColuna > posFinalColuna)
                    posInicialColuna--;
                else
                    posInicialColuna++;
                total++;
            }

            return total;
        }

        public static Tuple<int, int> CoordinatesOf(string[,] matrix, string value)
        {
            int w = matrix.GetLength(0); // width
            int h = matrix.GetLength(1); // height

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    if (matrix[x, y].Equals(value))
                        return Tuple.Create(x, y);
                }
            }

            return Tuple.Create(-1, -1);
        }

        public List<(int, int)> ChecarNumeroDeEstados(string[,] matrix)
        {
            var posicaoEmBranco = CoordinatesOf(matrix, " ");
            if (posicaoEmBranco.Equals(new Tuple<int, int>(0, 0)))
                return new List<(int, int)>()
                {
                    (0,1),
                    (1,0)
                };
            else if (posicaoEmBranco.Equals(new Tuple<int, int>(0, 1)))
                return new List<(int, int)>()
                {
                    (0,0),
                    (0,2),
                    (1,1)
                };
            else if (posicaoEmBranco.Equals(new Tuple<int, int>(0, 2)))
                return new List<(int, int)>()
                {
                    (0,1),
                    (1,2)
                };

            return new List<(int, int)>();

        }

    }
}
