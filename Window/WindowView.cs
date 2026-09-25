using ChessRules;
using tabuleiro;

namespace window;

public static class WindowView
{


    public static void ShowChessMatch(ChessMatch match)
    {
        ShowTabuleiro(match.Tabuleiro);
        ShowCapturedPieces(match);
        Console.WriteLine($"\nTurno: {match.Turno}");
        if (!match.FinishedMatch)
        {
            Console.WriteLine($"Aguardando Jogada: {match.CurrentPlayer}");   
            if(match.Check) Console.WriteLine("Xeque!");
        }
        else Console.WriteLine($"Xequemate!\nVencerdor: {match.CurrentPlayer}");
        
        
    }

    public static void ShowCapturedPieces(ChessMatch match)
    {
        Console.Write($"\nPeças Capturadas:\nBrancas: ");
        ShowPieceSet(match.CapturedPieces(Color.Branco));
        Console.Write($"\nPretas: ");
        ConsoleColor aux = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        ShowPieceSet(match.CapturedPieces(Color.Preto));
        Console.ForegroundColor = aux;
    }

    public static void ShowPieceSet(HashSet<Piece> set)
    {
        Console.Write("[");
        foreach(var item in set)
        {
            Console.Write(item + " ");
        }
        Console.Write("]");
        //Console.WriteLine($"[{string.Join(", ", set)}]");
    }

    public static void ShowTabuleiro(Tabuleiro tabuleiro)
    {
        Console.WriteLine("  A B C D E F G H");
        for (int i = 0; i < tabuleiro.Linha; i++)
        {
            Console.Write(8 - i + " ");
            for (int j = 0; j < tabuleiro.Coluna; j++)
            {
                ShowPiece(tabuleiro.FindPiece(i, j));
                //Console.Write(tabuleiro.FindPiece(i, j) + " ");

            }
            Console.WriteLine();
        }
    }

    public static void ShowTabuleiro(Tabuleiro tabuleiro, bool[,] possibleMoves)
    {
        ConsoleColor originalBackground = Console.BackgroundColor;
        ConsoleColor moveBackground = ConsoleColor.White;
        Console.WriteLine("  A B C D E F G H");
        for (int i = 0; i < tabuleiro.Linha; i++)
        {
            Console.Write(8 - i + " ");
            for (int j = 0; j < tabuleiro.Coluna; j++)
            {
                if(possibleMoves[i, j])
                {
                    Console.BackgroundColor = moveBackground;
                }
                else
                {
                    Console.BackgroundColor = originalBackground;
                }
                ShowPiece(tabuleiro.FindPiece(i, j));                
                Console.BackgroundColor = originalBackground;
            }
            Console.WriteLine();
        }
        Console.BackgroundColor = originalBackground;
    }

    public static ChessPosition SelectPieceThenPosition()
    {
        (char coluna, int linha) = Console.ReadLine() is var p ? (p[0], (int)char.GetNumericValue(p[1])) : default;
        return new ChessPosition(linha, coluna);
    }

    public static void ShowPiece(Piece piece)
    {
        if (piece == null) Console.Write("- ");
        else if (piece.Color == Color.Branco)
        {
            Console.Write(piece + " ");
        }
        else
        {
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(piece + " ");
            Console.ForegroundColor = aux;
        }      
    }
}