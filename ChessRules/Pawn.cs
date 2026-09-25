using tabuleiro;

namespace ChessRules;

public class Pawn(Tabuleiro tabuleiro, Color color, ChessMatch match): Piece(tabuleiro, color)
{
    private ChessMatch Match = match;

    private bool EnemyExist(Position position)
    {
        Piece p = Tabuleiro.FindPiece(position);
        return p != null && p.Color != Color;
    }
    public override bool[,] PossibleMoviment()
    {
        bool[,] mat = new bool[Tabuleiro.Linha, Tabuleiro.Coluna];
        Position position = new(0, 0);
        int direction = Color == Color.Branco ? -1 : 1;
        // North
        position.DefinirValores(Position!.Linha + direction, Position.Coluna);
        if (Tabuleiro.ValidPosition(position) && CanMove(position) && Tabuleiro.FindPiece(position) == null)
        {
            mat[position.Linha, position.Coluna] = true;
            if(QtdMovimentos == 0)
            {
                position.DefinirValores(Position!.Linha + (2 * direction), Position.Coluna);
                if (Tabuleiro.ValidPosition(position) && CanMove(position) && Tabuleiro.FindPiece(position) == null)
                    mat[position.Linha, position.Coluna] = true;
            }
        }
        // NE
        position.DefinirValores(Position!.Linha + direction, Position.Coluna + 1);
        if (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            if (Tabuleiro.FindPiece(position) is Piece p && p.Color != Color)
                mat[position.Linha, position.Coluna] = true;
        }
        // NW
        position.DefinirValores(Position.Linha + direction, Position.Coluna - 1);
        if (Tabuleiro.ValidPosition(position) && CanMove(position))
        {
            if (Tabuleiro.FindPiece(position) is Piece p && p.Color != Color)
                mat[position.Linha, position.Coluna] = true;
        }

        // Enpassant
        if((Position.Linha == 3 && Color == Color.Branco) || (Position.Linha == 4 && Color == Color.Preto))
        {
            Position esquerda = new(Position.Linha, Position.Coluna - 1);
            if(Tabuleiro.ValidPosition(esquerda) && EnemyExist(esquerda) && Tabuleiro.FindPiece(esquerda) == Match.EnPassantEnable) { mat[esquerda.Linha + direction, esquerda.Coluna] = true; }           
            
            Position direita = new(Position.Linha, Position.Coluna + 1);
            if(Tabuleiro.ValidPosition(direita) && EnemyExist(direita) && Tabuleiro.FindPiece(direita) == Match.EnPassantEnable) { mat[direita.Linha + direction, direita.Coluna] = true; }                       
        }


        return mat;
    }


    public override string ToString()
    {
        return "p";
    }
}