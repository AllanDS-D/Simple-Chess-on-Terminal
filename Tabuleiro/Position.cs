namespace tabuleiro;

public class Position(int linha, int coluna)
{
    public int Linha { get; set; } = linha;
    public int Coluna { get; set; } = coluna;

    public void DefinirValores(int linha, int coluna)
    {
        Linha = linha;
        Coluna = coluna;
    }
    public override string ToString()
    {
        return $"Position: {Linha}, {Coluna}";
    }
}