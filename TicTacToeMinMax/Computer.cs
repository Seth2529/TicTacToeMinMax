using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class Computer
    {
        public void JogadasMaquina(char[,] tabuleiro)
        {
            int melhorPontuacao = int.MinValue;
            int melhorLinha = -1;
            int melhorColuna = -1;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (tabuleiro[i, j] == ' ')
                    {
                        tabuleiro[i, j] = 'O';
                        int pontuacao = MinMax(tabuleiro, 0, false);
                        tabuleiro[i, j] = ' ';

                        if (pontuacao > melhorPontuacao)
                        {
                            melhorPontuacao = pontuacao;
                            melhorLinha = i;
                            melhorColuna = j;
                        }
                    }
                }
            }

            tabuleiro[melhorLinha, melhorColuna] = 'O';
            Console.WriteLine($"O Jogador O marcou na posição ({melhorLinha + 1}, {melhorColuna + 1}).");
        }

        private int MinMax(char[,] tabuleiro, int profundidade, bool isMaximizing)
        {
            int resultado = Avaliar(tabuleiro);
            if (resultado != 0) return resultado;
            if (tabuleiro.Cast<char>().All(p => p != ' ')) return 0;

            if (isMaximizing)
            {
                int melhorPontuacao = int.MinValue;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (tabuleiro[i, j] == ' ')
                        {
                            tabuleiro[i, j] = 'O';
                            int pontuacao = MinMax(tabuleiro, profundidade + 1, false);
                            tabuleiro[i, j] = ' ';

                            melhorPontuacao = Math.Max(melhorPontuacao, pontuacao);
                        }
                    }
                }

                return melhorPontuacao;
            }
            else
            {
                int piorPontuacao = int.MaxValue;

                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (tabuleiro[i, j] == ' ')
                        {
                            tabuleiro[i, j] = 'X';
                            int pontuacao = MinMax(tabuleiro, profundidade + 1, true);
                            tabuleiro[i, j] = ' ';

                            piorPontuacao = Math.Min(piorPontuacao, pontuacao);
                        }
                    }
                }

                return piorPontuacao;
            }
        }

        private int Avaliar(char[,] tabuleiro)
        {
            if (Vencer('O', tabuleiro)) return 1;
            if (Vencer('X', tabuleiro)) return -1;
            return 0;
        }

        private bool Vencer(char jogador, char[,] tabuleiro)
        {
            for (int i = 0; i < 3; i++)
            {
                if (tabuleiro[i, 0] == jogador && tabuleiro[i, 1] == jogador && tabuleiro[i, 2] == jogador)
                {
                    return true;
                }
            }
            for (int j = 0; j < 3; j++)
            {
                if (tabuleiro[0, j] == jogador && tabuleiro[1, j] == jogador && tabuleiro[2, j] == jogador)
                {
                    return true;
                }
            }
            if (tabuleiro[0, 0] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 2] == jogador)
            {
                return true;
            }
            if (tabuleiro[0, 2] == jogador && tabuleiro[1, 1] == jogador && tabuleiro[2, 0] == jogador)
            {
                return true;
            }
            return false;
        }
    }

}