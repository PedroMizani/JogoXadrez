using System;
using tabuleiro;
using Xadrez;

namespace Xadrez_console
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                PartidaDeXadrez partida = new PartidaDeXadrez();

                while (!partida.Terminada)
                {

                    try
                    {
                        Console.Clear();
                        Tela.ImprimirPartida(partida);


                        Console.WriteLine();

                        Console.Write("Digite a posição de origem: ");
                        Posicao origem = Tela.lerPosicaoXadrez().toPosicao();

                        partida.ValidarPosicaoDeOrigem(origem);

                        bool[,] posicoesPossiveis = partida.Tab.peca(origem).MovimentosPossiveis();


                        Console.Clear();
                        Tela.ImprimirTabuleiro(partida.Tab, posicoesPossiveis);

                        Console.WriteLine();
                        Console.Write("Digite a posição de destino: ");
                        Posicao destino = Tela.lerPosicaoXadrez().toPosicao();

                        partida.ValidarPosicaoDeDestino(origem, destino); 

                        partida.RealizarJogada(origem, destino);

                        
                    }
                    catch (TabuleiroException e) 
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }


                Tela.ImprimirTabuleiro(partida.Tab);

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();

        }
    }
}
