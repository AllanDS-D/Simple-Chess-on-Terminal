using tabuleiro;

namespace ChessRules;

public class Knight(Tabuleiro tabuleiro, Color color) : Piece(tabuleiro, color)
{
    public override bool[,] PossibleMoviment()
    {
        bool[,] mat = new bool[Tabuleiro.Linha, Tabuleiro.Coluna];
        Position position = new(0, 0);

        // North W
        position.DefinirValores(Position!.Linha - 2, Position.Coluna - 1);
        if (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
        }
        // North E
        position.DefinirValores(Position!.Linha - 2, Position.Coluna + 1);
        if (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
        }
        // East N
        position.DefinirValores(Position!.Linha - 1, Position.Coluna + 2);
        if (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
        }
        // East S
        position.DefinirValores(Position!.Linha + 1, Position.Coluna + 2);
        if (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
        }
        // South E
        position.DefinirValores(Position!.Linha + 2, Position.Coluna + 1);
        if (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
        }
        // South W
        position.DefinirValores(Position!.Linha + 2, Position.Coluna - 1);
        if (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
        }
        // West S
        position.DefinirValores(Position!.Linha + 1, Position.Coluna - 2);
        if (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
        }
        // West N
        position.DefinirValores(Position!.Linha - 1, Position.Coluna - 2);
        if (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            mat[position.Linha, position.Coluna] = true;
        }
        return mat;
    }

    public override string ToString()
    {
        return "N";
    }
}