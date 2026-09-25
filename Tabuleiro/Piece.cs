namespace tabuleiro;

public abstract class Piece(Tabuleiro tabuleiro, Color color)
{
    public Position? Position { get; set; } = null;
    public Color Color { get; protected set; } = color;
    public Tabuleiro Tabuleiro { get; protected set; } = tabuleiro;
    public int QtdMovimentos { get; protected set; } = 0;

    public abstract bool[,] PossibleMoviment();   
   
    protected bool CanMove(Position position)
    {
        Piece piece = Tabuleiro.FindPiece(position);
        //return piece == null && piece.Color != Color;
        return piece?.Color != Color;
    } 
     public void IncrementQtdMovimentos()
    {
        QtdMovimentos++;
    }

    public void DecementQtdMovimentos()
    {
        QtdMovimentos--;
    }

    public bool ExistPossibleMoviment()
    {
        bool[,] mat = PossibleMoviment();
        for(int i = 0; i < Tabuleiro.Linha; i++)
        {
            for(int j = 0; j < Tabuleiro.Coluna; j++)
            {
                if(mat[i, j]) return true;
            }
        }        
        return false;
    }

    public bool CanMoveTo(Position position)
    {
        return PossibleMoviment()[position.Linha, position.Coluna];
    }


}