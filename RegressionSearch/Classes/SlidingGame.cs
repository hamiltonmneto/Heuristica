﻿using Heuristica.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RegressionSearch.Classes
{
    public class SlidingGame
    {
        public string[,] estadofinal = { { "1", "2", "3" }, { "4", "5", "6" }, { "7", "8", " " } };

        public string[,] estadoFinalTeste = { { "1", "2", "3" }, { "8", " ", "4" }, { "7", "6", "5" } };
        public string[,] estadoInicialTeste = { { " ", "1", "2" }, { "7", "8", "3" }, { "6", "5", "4" } };


        public List<string> NewState(List<string> estadoInicial)
        {
            var estadoInicialPreparado = PreparaEstadoInicial(estadoInicial);
            estadoInicialPreparado = estadoInicialTeste;
            return ConstruirNovoEstado(estadoInicialPreparado);
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

        public List<string> ConstruirNovoEstado(string[,] estadoinicial)
        {
            
            var numeroDeEstados = ChecarNumeroDeEstados(estadoinicial);
            var estadoNo = new EstadoNo()
            {
                estadoAtual = estadoinicial,
                heuristica = GetHeuristica(estadoinicial),
                estadosFilhos = gerarEstadosFilho(estadoinicial, numeroDeEstados)
            };


            if (estadoNo.heuristica != 0)
                return GerarNovosFilhos(estadoNo);

            return estadoNo.estadoAtual.Cast<string>().ToList();
        }

        private List<string> GerarNovosFilhos(EstadoNo estadoNo)
        {
            var melhorFilho = estadoNo.estadosFilhos.OrderBy(x => x.heuristica).First();
            var numeroDeEstados = ChecarNumeroDeEstados(melhorFilho.estadoAtual);
            var novoNo = new EstadoNo()
            {
                estadoAtual = melhorFilho.estadoAtual,
                heuristica = GetHeuristica(melhorFilho.estadoAtual),
                estadosFilhos = gerarEstadosFilho(melhorFilho.estadoAtual, numeroDeEstados)
            };

            melhorFilho.estadosFilhos = novoNo.estadosFilhos;

            if (novoNo.heuristica != 0)
                return GerarNovosFilhos(novoNo);
            var estadoOrdenado = novoNo.estadoAtual.Cast<string>().ToList();
            return novoNo.estadoAtual.Cast<string>().ToList();
        }

        private int GetHeuristica(string[,] estadoinicial)
        {
            var heuristica = 0; 
            for (int i = 1; i <= estadoinicial.Length + 1; i++)
            {
                var posicaoElementoEstadoInicial = CoordinatesOf(estadoinicial, i.ToString());
                var posicaoElementoEstadoFinal = CoordinatesOf(estadoFinalTeste, i.ToString());
                //var posicaoElementoEstadoFinal = CoordinatesOf(estadofinal, i.ToString());
                heuristica += CalculaHeuristica(posicaoElementoEstadoInicial, posicaoElementoEstadoFinal);
            }

            return heuristica;
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
            else if (posicaoEmBranco.Equals(new Tuple<int, int>(1, 0)))
                return new List<(int, int)>()
                {
                    (0,0),
                    (1,1),
                    (2,0)
                };
            else if (posicaoEmBranco.Equals(new Tuple<int, int>(1, 1)))
                return new List<(int, int)>()
                {
                    (0,1),
                    (1,0),
                    (2,1),
                    (1,2),
                };
            else if (posicaoEmBranco.Equals(new Tuple<int, int>(1, 2)))
                return new List<(int, int)>()
                {
                    (0,2),
                    (1,1),
                    (2,2)
                };
            else if (posicaoEmBranco.Equals(new Tuple<int, int>(2, 0)))
                return new List<(int, int)>()
                {
                    (1,0),
                    (2,1)
                };
            else if (posicaoEmBranco.Equals(new Tuple<int, int>(2, 1)))
                return new List<(int, int)>()
                {
                    (2,0),
                    (1,1),
                    (2,2)
                };
            else if (posicaoEmBranco.Equals(new Tuple<int, int>(2, 2)))
                return new List<(int, int)>()
                {
                    (1,2),
                    (2,1)
                };

                return new List<(int, int)>();

        }


        public List<EstadoNo> gerarEstadosFilho(string[,] estadoinicial, List<(int, int)>posProximosEstados)
        {
            var espacoBrancoCord = CoordinatesOf(estadoinicial, " ");
            var listaFilhos = new List<EstadoNo>();
            for (int i = 0; i < posProximosEstados.Count; i++)
            {
                var estadoTemp = estadoinicial.Clone() as string[,];
                var valorASubstituirEspacoBranco = estadoTemp.GetValue(posProximosEstados[i].Item1, posProximosEstados[i].Item2);
                Movimentar(estadoTemp, valorASubstituirEspacoBranco.ToString(), posProximosEstados[i], espacoBrancoCord);

                var estadoNo = new EstadoNo()
                {
                    estadoAtual = estadoTemp,
                    heuristica = GetHeuristica(estadoTemp)
                };

                listaFilhos.Add(estadoNo);
            }
            return listaFilhos;
        }

        private void Movimentar(string[,] estadoTemp, string valorASubstituirEspacoBranco, (int, int) posProximosEstados, Tuple<int, int> espacoBrancoCord)
        {
            estadoTemp[espacoBrancoCord.Item1, espacoBrancoCord.Item2] = valorASubstituirEspacoBranco.ToString();
            estadoTemp[posProximosEstados.Item1, posProximosEstados.Item2] = " ";
        }

    }
}
