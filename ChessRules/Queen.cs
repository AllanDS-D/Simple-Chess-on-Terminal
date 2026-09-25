using tabuleiro;

namespace ChessRules;

public class Queen(Tabuleiro tabuleiro, Color color) : Piece(tabuleiro, color)
{
    public override bool[,] PossibleMoviment()
    {
        bool[,] mat = new bool[Tabuleiro.Linha, Tabuleiro.Coluna];
        Position position = new(0, 0);

        // North
        position.DefinirValores(Position!.Linha - 1, Position.Coluna);
        while (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
            if (Tabuleiro.FindPiece(position) is Piece p && p.Color != Color) { break; }
            position.Linha--;
        }
        // NE
        position.DefinirValores(Position.Linha - 1, Position.Coluna + 1);
        while (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
            if (Tabuleiro.FindPiece(position) is Piece p && p.Color != Color) { break; }
            position.Linha--;
            position.Coluna++;
        }
        // East
        position.DefinirValores(Position.Linha, Position.Coluna + 1);
        while (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
            if (Tabuleiro.FindPiece(position) is Piece p && p.Color != Color) { break; }
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
        // South
        position.DefinirValores(Position.Linha + 1, Position.Coluna);
        while (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
            if (Tabuleiro.FindPiece(position) is Piece p && p.Color != Color) { break; }
            position.Linha++;
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
        // West
        position.DefinirValores(Position.Linha, Position.Coluna - 1);
        while (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
            if (Tabuleiro.FindPiece(position) is Piece p && p.Color != Color) { break; }
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
        return "Q";
    }
}