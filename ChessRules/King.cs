using System.Text.RegularExpressions;
using tabuleiro;

namespace ChessRules;

public class King(Tabuleiro tabuleiro, Color color, ChessMatch match) : Piece(tabuleiro, color)
{
    private ChessMatch Match = match;

    private bool IsTowerAbleToCastling(Position position)
    {
        Piece p = Tabuleiro.FindPiece(position);
        return p != null && p is Tower && p.Color == Color && p.QtdMovimentos == 0;
    }
    public override bool[,] PossibleMoviment()
    {
        bool[,] mat = new bool[Tabuleiro.Linha, Tabuleiro.Coluna];
        Position position = new(0, 0);

        // Norte
        position.DefinirValores(Position!.Linha - 1, Position.Coluna);
        if (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
        }
        // NE
        position.DefinirValores(Position.Linha - 1, Position.Coluna + 1);
        if (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
        }
        // East
        position.DefinirValores(Position.Linha, Position.Coluna + 1);
        if (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
        }
        // SE
        position.DefinirValores(Position.Linha + 1, Position.Coluna + 1);
        if (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
        }
        // South
        position.DefinirValores(Position.Linha + 1, Position.Coluna);
        if (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
        }
        // SW
        position.DefinirValores(Position.Linha + 1, Position.Coluna - 1);
        if (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
        }
        // West
        position.DefinirValores(Position.Linha, Position.Coluna - 1);
        if (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
        }
        // NW
        position.DefinirValores(Position.Linha - 1, Position.Coluna - 1);
        if (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
        }

        // #Roque
        if (QtdMovimentos == 0 && !Match.Check)
        {
            // Roque Pequeno
            Position towerPosition = new(Position.Linha, Position.Coluna + 3);
            if (IsTowerAbleToCastling(towerPosition))
            {
                Position p1 = new(Position.Linha, Position.Coluna + 1);
                Position p2 = new(Position.Linha, Position.Coluna + 2);
                if(Tabuleiro.FindPiece(p1) == null && Tabuleiro.FindPiece(p2) == null) mat[Position.Linha, Position.Coluna + 2] = true;
            }
            // Roque Grande
            Position towerPosition2 = new(Position.Linha, Position.Coluna - 4);
            if (IsTowerAbleToCastling(towerPosition2))
            {
                Position p1 = new(Position.Linha, Position.Coluna - 1);
                Position p2 = new(Position.Linha, Position.Coluna - 2);
                Position p3 = new(Position.Linha, Position.Coluna - 3);
                if(Tabuleiro.FindPiece(p1) == null && Tabuleiro.FindPiece(p2) == null && Tabuleiro.FindPiece(p3) == null) mat[Position.Linha, Position.Coluna - 2] = true;
            }
        }

        return mat;
    }

    public override string ToString()
    {
        return "K";
    }
}