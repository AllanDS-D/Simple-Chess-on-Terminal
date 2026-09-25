using ChessRules;
using tabuleiro;
using tabuleiro.Exception;
using static window.WindowView;

namespace Xadrez.C;

public class Xadrez
{
    public static void Main(string[] args)
    {
        try
        {
            var match = new ChessMatch();

            while (!match.FinishedMatch)
            {
                try
                {
                    Console.Clear();
                    ShowChessMatch(match);
                    Console.Write("Origem: ");
                    Position start = SelectPieceThenPosition().ToBoardPosition();
                    match.ValidPositionOfOrigin(start);

                    bool[,] possibleMoves = match.Tabuleiro.FindPiece(start).PossibleMoviment();
                    Console.Clear();
                    ShowTabuleiro(match.Tabuleiro, possibleMoves);

                    Console.Write("Destino: ");
                    Position end = SelectPieceThenPosition().ToBoardPosition();
                    match.ValidPositionOfDestination(start, end);

                    match.MakeA_Move(start, end);
                }
                catch (TabuleiroException e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadLine();
                }
            }
            Console.Clear();
            ShowChessMatch(match);
        }
        catch (TabuleiroException e)
        {
            Console.WriteLine(e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }




    }
}
