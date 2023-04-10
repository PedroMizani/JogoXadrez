namespace tabuleiro
{
    abstract class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdeMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; protected set; }
        public Peca () { }

        public Peca( Cor cor, Tabuleiro tabuleiro)
        {
            Posicao = null;
            Cor = cor;
            Tabuleiro = tabuleiro;
            QtdeMovimentos = 0;
        }

        public abstract bool[,] MovimentosPossiveis();
       

        public void IncrementarQtdeMovimento()
        {
            QtdeMovimentos++;
        }
    }
}
