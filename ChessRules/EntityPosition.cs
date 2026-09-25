using tabuleiro;

namespace ChessRules;

public class ChessPosition(int linha, char coluna)
{
    public int Linha { get; set; } = linha;
    public char Coluna { get; set; } = coluna;

    public Position ToBoardPosition()
    {
        return new Position(8 - Linha, Coluna - 'a');
    }

    public override string ToString()
    {
        return $"{Coluna}{Linha}";
    }
}