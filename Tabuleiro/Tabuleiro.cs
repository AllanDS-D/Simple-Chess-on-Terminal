using tabuleiro.Exception;

namespace tabuleiro;

public class Tabuleiro(int linha, int coluna)
{
    public int Linha { get; set; } = linha;
    public int Coluna { get; set; } = coluna;
    private Piece[,] Pieces = new Piece[linha, coluna];

    public Piece FindPiece(int linha, int coluna)
    {
        return Pieces[linha, coluna];
    }

    public Piece FindPiece(Position position) // Sobrecarga do method FindPiece
    {
        return Pieces[position.Linha, position.Coluna];
    }

    public void PlaceAPiece(Piece piece, Position position)
    {
        if(PieceExist(position)) throw new TabuleiroException("A piece already exist in this position!");
        Pieces[position.Linha, position.Coluna] = piece;
        piece.Position = position;
    }

    public Piece RemovePiece(Position position)
    {
        if(FindPiece(position) == null) return null!;
        Piece aux = FindPiece(position);
        Pieces[position.Linha, position.Coluna] = null!;
        return aux;
    }


    public bool PieceExist(Position position)
    {
        PositionIsValid(position);
        return FindPiece(position) != null;
    }

    public bool ValidPosition(Position position)
    {
        if (position.Linha < 0 || position.Linha >= Linha || position.Coluna < 0 || position.Coluna >= Coluna)
            return false;
        return true;
    }

    public void PositionIsValid(Position position)
    {
        if (!ValidPosition(position)) throw new TabuleiroException("Position is invalid!");
    }


}