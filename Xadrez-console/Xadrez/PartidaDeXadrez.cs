﻿using System;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using tabuleiro;


namespace Xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }

        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro (8,8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            ColocarPecas();
            
        }

        public void ExecutaMovimento (Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca (origem);
            p.IncrementarQtdeMovimento();
            Peca Pecacapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);
        }

        public void RealizarJogada (Posicao origrem, Posicao destino)
        {
            ExecutaMovimento(origrem, destino);
            Turno++;
            MudaJogador();
        }

        public void ValidarPosicaoDeOrigem (Posicao posicao)
        {
            if (Tab.peca(posicao) == null)
            {
                throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
            }

            if (JogadorAtual != Tab.peca(posicao).Cor)
            {
                throw new TabuleiroException("A peça de origem escolhida não é sua!");
            }

            if (!Tab.peca (posicao).ExisteMovimentosPossiveis())
            {
                throw new TabuleiroException("Não há movimentos possiveis para a peça de origem escolhida!");
            }
        }

        public void ValidarPosicaoDeDestino (Posicao origem, Posicao destino)
        {
            if (!Tab.peca(origem).PodeMoverPara(destino))
            {
                throw new TabuleiroException("Posição de destino invalida!");
            }
        }

        private void MudaJogador()
        {
            if (JogadorAtual == Cor.Branca)
            {
                JogadorAtual = Cor.Preta;
            }
            else
            {
                JogadorAtual = Cor.Branca;
            }
        }

        private void ColocarPecas()
        {
            Tab.ColocarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('c',1).toPosicao());
            Tab.ColocarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('c', 2).toPosicao());
            Tab.ColocarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('d', 2).toPosicao());
            Tab.ColocarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('e', 2).toPosicao());
            Tab.ColocarPeca(new Torre(Cor.Branca, Tab), new PosicaoXadrez('e', 1).toPosicao());
            Tab.ColocarPeca(new Rei(Cor.Branca, Tab), new PosicaoXadrez('d', 1).toPosicao());

            Tab.ColocarPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('c', 7).toPosicao());
            Tab.ColocarPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('c', 8).toPosicao());
            Tab.ColocarPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('d', 7).toPosicao());
            Tab.ColocarPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('e', 7).toPosicao());
            Tab.ColocarPeca(new Torre(Cor.Preta, Tab), new PosicaoXadrez('e', 8).toPosicao());
            Tab.ColocarPeca(new Rei(Cor.Preta, Tab), new PosicaoXadrez('d', 8).toPosicao());

        }
    }
}
