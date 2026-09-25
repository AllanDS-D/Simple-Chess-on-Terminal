using tabuleiro;

namespace ChessRules;

public class Tower(Tabuleiro tabuleiro, Color color) : Piece(tabuleiro, color)
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
            // Se encontrou uma peça (que pelo CanMove sabemos que é inimiga), para o loop após marcar como válida
            if (Tabuleiro.FindPiece(position) is Piece p && p.Color != Color) { break; }
            position.Linha--;
        }
        // East
        position.DefinirValores(Position.Linha, Position.Coluna + 1);
        while (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
            if (Tabuleiro.FindPiece(position) is Piece p && p.Color != Color) { break; }
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
        // West
        position.DefinirValores(Position.Linha, Position.Coluna - 1);
        while (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
            if (Tabuleiro.FindPiece(position) is Piece p && p.Color != Color) { break; }
            position.Coluna--;
        }

        return mat;
    }

    public override string ToString()
    {
        return "T";
    }
}