using System;
using System.Net.Http.Headers;
using System.Security.Cryptography.X509Certificates;
using tabuleiro;
using System.Collections.Generic;


namespace Xadrez
{
    internal class PartidaDeXadrez
    {
        public Tabuleiro Tab { get; private set; }
        public int Turno { get; private set; }
        public Cor JogadorAtual { get; private set; }
        public bool Terminada { get; private set; }

        private HashSet<Peca> pecas;
        private HashSet<Peca> capturadas;



        public PartidaDeXadrez()
        {
            Tab = new Tabuleiro (8,8);
            Turno = 1;
            JogadorAtual = Cor.Branca;
            Terminada = false;
            pecas = new HashSet<Peca> ();
            capturadas = new HashSet<Peca> ();
            ColocarPecas();

            
        }

        public void ExecutaMovimento (Posicao origem, Posicao destino)
        {
            Peca p = Tab.RetirarPeca (origem);
            p.IncrementarQtdeMovimento();
            Peca Pecacapturada = Tab.RetirarPeca(destino);
            Tab.ColocarPeca(p, destino);

            if (Pecacapturada != null) 
            { 
                capturadas.Add(Pecacapturada);
            }
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
                throw new TabuleiroException("Posição de destino inválida!");
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


        public HashSet<Peca> PecasCapturadas(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca> ();
            foreach (Peca x in capturadas)
            {
                if (x.Cor == cor)
                {
                    aux.Add (x);
                }
            }
            return aux;
        }

        public HashSet<Peca> PecasEmJogo(Cor cor)
        {
            HashSet<Peca> aux = new HashSet<Peca>();
            foreach (Peca x in pecas)
            {
                if (x.Cor == cor)
                {
                    aux.Add(x);
                }
            }
            aux.ExceptWith(PecasCapturadas(cor));
            return aux;

        }

        public void ColocarNovaPeca(char colune, int linha, Peca peca)
        {
            Tab.ColocarPeca(peca, new PosicaoXadrez(colune, linha).toPosicao());
            pecas.Add(peca);
        }

        private void ColocarPecas()
        {
            ColocarNovaPeca('c', 1, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('c', 2, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('d', 2, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('e', 2, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('e', 1, new Torre(Cor.Branca, Tab));
            ColocarNovaPeca('d', 1, new Rei(Cor.Branca, Tab));

            ColocarNovaPeca('c', 7, new Torre(Cor.Preta, Tab));
            ColocarNovaPeca('c', 8, new Torre(Cor.Preta, Tab));
            ColocarNovaPeca('d', 7, new Torre(Cor.Preta, Tab));
            ColocarNovaPeca('e', 7, new Torre(Cor.Preta, Tab));
            ColocarNovaPeca('e', 8, new Torre(Cor.Preta, Tab));
            ColocarNovaPeca('d', 8, new Rei(Cor.Preta, Tab));

        }
    }
}
