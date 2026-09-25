using tabuleiro;

namespace ChessRules;

public class Bishop(Tabuleiro tabuleiro, Color color) : Piece(tabuleiro, color)
{
    public override bool[,] PossibleMoviment()
    {
        bool[,] mat = new bool[Tabuleiro.Linha, Tabuleiro.Coluna];
        Position position = new(0, 0);
        // NE
        position.DefinirValores(Position!.Linha - 1, Position.Coluna + 1);
        while (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
            if (Tabuleiro.FindPiece(position) is Piece p && p.Color != Color) { break; }
            position.Linha--;
            position.Coluna++;
        }
        // SE
        position.DefinirValores(Position.Linha + 1, Position.Coluna + 1);
        while (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
            if (Tabuleiro.FindPiece(position) is Piece p && p.Color != Color) { break; }
            position.Linha++;
            position.Coluna++;
        }
        // SW
        position.DefinirValores(Position.Linha + 1, Position.Coluna - 1);
        while (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
            if (Tabuleiro.FindPiece(position) is Piece p && p.Color != Color) { break; }
            position.Linha++;
            position.Coluna--;
        }
        // NW
        position.DefinirValores(Position.Linha - 1, Position.Coluna - 1);
        while (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
            if (Tabuleiro.FindPiece(position) is Piece p && p.Color != Color) { break; }
            position.Linha--;
            position.Coluna--;
        }

        return mat;
    }

    public override string ToString()
    {
        return "B";
    }
}