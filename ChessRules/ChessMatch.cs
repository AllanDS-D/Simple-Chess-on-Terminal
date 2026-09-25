using System.Security.Cryptography.X509Certificates;
using tabuleiro;
using tabuleiro.Exception;

namespace ChessRules;

public class ChessMatch
{
    public Tabuleiro Tabuleiro { get; private set; }
    public int Turno { get; private set; }
    public Color CurrentPlayer { get; private set; }
    public bool FinishedMatch { get; private set; }
    public bool Check { get; private set; }
    private HashSet<Piece> Pieces;
    private HashSet<Piece> Captured;

    public Piece EnPassantEnable;

    public ChessMatch()
    {
        Tabuleiro = new Tabuleiro(8, 8);
        Turno = 1;
        CurrentPlayer = Color.Branco;
        FinishedMatch = false;
        Check = false;
        EnPassantEnable = null!;
        Pieces = [];
        Captured = [];
        PlaceAllPieces();
    }

    public Piece MovePiece(Position origem, Position destino)
    {
        Piece p = Tabuleiro.RemovePiece(origem);
        p.IncrementQtdMovimentos();
        Piece CapturedPiece = Tabuleiro.RemovePiece(destino);
        Tabuleiro.PlaceAPiece(p, destino);
        if (CapturedPiece != null) Captured.Add(CapturedPiece);

        // #Jogada Especial Roque pequeno
        if (p is King && destino.Coluna == origem.Coluna + 2)
        {
            Position origemT = new(origem.Linha, origem.Coluna + 3);
            Position destinoT = new(origem.Linha, origem.Coluna + 1);
            Piece t = Tabuleiro.RemovePiece(origemT);
            t.IncrementQtdMovimentos();
            Tabuleiro.PlaceAPiece(t, destinoT);
        }

        // #Jogada Especial Roque Grande
        if (p is King && destino.Coluna == origem.Coluna - 2)
        {
            Position origemT = new(origem.Linha, origem.Coluna - 4);
            Position destinoT = new(origem.Linha, origem.Coluna - 1);
            Piece t = Tabuleiro.RemovePiece(origemT);
            t.IncrementQtdMovimentos();
            Tabuleiro.PlaceAPiece(t, destinoT);
        }

        // Jogada Especial Enpassant
        if (p is Pawn)
        {
            if (origem.Coluna != destino.Coluna && CapturedPiece == null)
            {
                Position enPassant;
                if (p.Color == Color.Branco)
                    enPassant = new(destino.Linha + 1, destino.Coluna);
                else
                    enPassant = new(destino.Linha - 1, destino.Coluna);
                CapturedPiece = Tabuleiro.RemovePiece(enPassant);
                Captured.Add(CapturedPiece);
            }
        }
        return CapturedPiece;
    }

    public void MakeA_Move(Position origem, Position destino)
    {
        Piece capturedPiece = MovePiece(origem, destino);
        if (IsKingInCheck(CurrentPlayer))
        {
            UnmovePiece(origem, destino, capturedPiece);
            throw new TabuleiroException("Você não pode se colocar em xeque!");
        }
        
        Piece p = Tabuleiro.FindPiece(destino);
        // Jogada Epsecial Promção
        if(p is Pawn)
        {
            if((p.Color == Color.Branco && destino.Linha == 0) ||(p.Color == Color.Preto && destino.Linha == 7))
            {
                p = Tabuleiro.RemovePiece(destino);
                Pieces.Remove(p);
                Console.Write("\nEscolha uma das Opções:\nQ - Rainha\nT - Torre\nB - Bispo\nN - Cavalo\n");
                Piece pieceOfChoice = char.Parse(Console.ReadLine()!.ToUpper()) switch
                {
                  'T' => new Tower(Tabuleiro, p.Color),
                  'B' => new Bishop(Tabuleiro, p.Color),
                  'N' => new Knight(Tabuleiro, p.Color),
                   _ => new Queen(Tabuleiro, p.Color),
                };                
                Tabuleiro.PlaceAPiece(pieceOfChoice, destino);
                Pieces.Add(pieceOfChoice);
            }
        }

        if (IsKingInCheck(Adversary(CurrentPlayer))) { Check = true; }
        else { Check = false; }

        if (isCheckmate(Adversary(CurrentPlayer))) { FinishedMatch = true; }
        else
        {
            Turno++;
            ChangePLayer();
        }

        // Enpassant - CORRIGIDO ✅
        
        if (p is Pawn && (destino.Linha == origem.Linha - 2 || destino.Linha == origem.Linha + 2))
        {
            EnPassantEnable = p;
        }
        else
        {
            EnPassantEnable = null!;
        }


        /*Turno++;
        ChangePLayer();*/
    }

    public void UnmovePiece(Position origem, Position destino, Piece capturedPiece)
    {
        Piece p = Tabuleiro.RemovePiece(destino);
        p.DecementQtdMovimentos();
        if (capturedPiece != null)
        {
            Tabuleiro.PlaceAPiece(capturedPiece, destino);
            Captured.Remove(capturedPiece);
        }
        Tabuleiro.PlaceAPiece(p, origem);

        // #Jogada Especial Roque pequeno
        if (p is King && destino.Coluna == origem.Coluna + 2)
        {
            Position origemT = new(origem.Linha, origem.Coluna + 3);
            Position destinoT = new(origem.Linha, origem.Coluna + 1);
            Piece t = Tabuleiro.RemovePiece(destinoT);
            t.DecementQtdMovimentos();
            Tabuleiro.PlaceAPiece(t, origemT);
        }

        // #Jogada Especial Roque Grande
        if (p is King && destino.Coluna == origem.Coluna - 2)
        {
            Position origemT = new(origem.Linha, origem.Coluna - 4);
            Position destinoT = new(origem.Linha, origem.Coluna - 1);
            Piece t = Tabuleiro.RemovePiece(destinoT);
            t.DecementQtdMovimentos();
            Tabuleiro.PlaceAPiece(t, origemT);
        }

        // Jogada Especial Enpassant no UnmovePiece - FORMA MAIS SEGURA ✅
        if (p is Pawn)
        {
            // Se moveu na diagonal mas a peça capturada foi tirada da linha lateral (En Passant)
            if (origem.Coluna != destino.Coluna && capturedPiece != null && destino.Linha != capturedPiece.Position!.Linha)
            {
                // Devolve o peão capturado para a casa correta dele (linha 3 para brancas, linha 4 para pretas)
                Position posPeaoInimigo = new(p.Color == Color.Branco ? 3 : 4, destino.Coluna);
                Tabuleiro.PlaceAPiece(capturedPiece, posPeaoInimigo);
                Captured.Remove(capturedPiece);
            }
        }

    }

    public HashSet<Piece> CapturedPieces(Color color)
    {
        HashSet<Piece> aux = [];
        foreach (var captured in Captured)
        {
            if (captured.Color == color) aux.Add(captured);
        }
        return aux;
    }

    public HashSet<Piece> PiecesInGame(Color color)
    {
        HashSet<Piece> aux = [];
        foreach (var piece in Pieces)
        {
            if (piece.Color == color) aux.Add(piece);
        }
        aux.ExceptWith(CapturedPieces(color));
        return aux;
    }

    public void PlaceNewPiece(int linha, char coluna, Piece piece)
    {
        Tabuleiro.PlaceAPiece(piece, new ChessPosition(linha, coluna).ToBoardPosition());
        Pieces.Add(piece);
    }

    public void ValidPositionOfOrigin(Position position)
    {
        if (Tabuleiro.FindPiece(position) == null) throw new TabuleiroException("Não existe peça na posição de origem escolhida!");
        if (CurrentPlayer != Tabuleiro.FindPiece(position).Color) throw new TabuleiroException("A peça de origem escolhida não é sua!");
        if (!Tabuleiro.FindPiece(position).ExistPossibleMoviment()) throw new TabuleiroException("Não há movimentos possiveis para a peça de origem escolhida!");

    }

    public void ValidPositionOfDestination(Position origem, Position destino)
    {
        if (!Tabuleiro.FindPiece(origem).CanMoveTo(destino)) throw new TabuleiroException("Posição de destino inválida");
    }

    public void ChangePLayer() => CurrentPlayer = (CurrentPlayer == Color.Branco) ? Color.Preto : Color.Branco;


    public bool IsKingInCheck(Color color)
    {
        Piece R = KingColor(color) ?? throw new TabuleiroException($"Não existe rei da cor {color} no tabuleiro");
        foreach (Piece x in PiecesInGame(Adversary(color)))
        {
            bool[,] mat = x.PossibleMoviment();
            if (mat[R.Position!.Linha, R.Position.Coluna]) return true;
        }
        return false;
    }

    public bool isCheckmate(Color color)
    {
        if (!IsKingInCheck(color)) return false;
        foreach (Piece x in PiecesInGame(color))
        {
            bool[,] mat = x.PossibleMoviment();
            for (int i = 0; i < Tabuleiro.Linha; i++)
            {
                for (int j = 0; j < Tabuleiro.Coluna; j++)
                {
                    if (mat[i, j])
                    {
                        Position origem = x.Position!;
                        Position destino = new(i, j);
                        Piece capturedPiece = MovePiece(origem, destino);
                        UnmovePiece(origem, destino, capturedPiece);
                        if (!IsKingInCheck(color)) return false;
                    }
                }
            }
        }
        return true;
    }

    private Piece KingColor(Color color)
    {
        foreach (Piece x in PiecesInGame(color))
        {
            if (x is King) return x;
        }
        return null!;
    }

    private void PlaceAllPieces()
    {
        /*PlaceNewPiece(1, 'a', new Tower(Tabuleiro, Color.Branco));
        PlaceNewPiece(1, 'h', new Tower(Tabuleiro, Color.Branco));
        PlaceNewPiece(8, 'a', new Tower(Tabuleiro, Color.Preto));        
        PlaceNewPiece(1, 'e', new King(Tabuleiro, Color.Branco, this));
        PlaceNewPiece(8, 'e', new King(Tabuleiro, Color.Preto, this));*/
        // Peças Diferentes Brancos
        PlaceNewPiece(1, 'a', new Tower(Tabuleiro, Color.Branco));
        PlaceNewPiece(1, 'b', new Knight(Tabuleiro, Color.Branco));
        PlaceNewPiece(1, 'c', new Bishop(Tabuleiro, Color.Branco));
        PlaceNewPiece(1, 'd', new Queen(Tabuleiro, Color.Branco));
        PlaceNewPiece(1, 'e', new King(Tabuleiro, Color.Branco, this));
        PlaceNewPiece(1, 'f', new Bishop(Tabuleiro, Color.Branco));
        PlaceNewPiece(1, 'g', new Knight(Tabuleiro, Color.Branco));
        PlaceNewPiece(1, 'h', new Tower(Tabuleiro, Color.Branco));
        //Peões Brancos
        PlaceNewPiece(2, 'a', new Pawn(Tabuleiro, Color.Branco, this));
        PlaceNewPiece(2, 'b', new Pawn(Tabuleiro, Color.Branco, this));
        PlaceNewPiece(2, 'c', new Pawn(Tabuleiro, Color.Branco, this));
        PlaceNewPiece(2, 'd', new Pawn(Tabuleiro, Color.Branco, this));
        PlaceNewPiece(2, 'e', new Pawn(Tabuleiro, Color.Branco, this));
        PlaceNewPiece(2, 'f', new Pawn(Tabuleiro, Color.Branco, this));
        PlaceNewPiece(2, 'g', new Pawn(Tabuleiro, Color.Branco, this));
        PlaceNewPiece(2, 'h', new Pawn(Tabuleiro, Color.Branco, this));
        // Peças Diferentes Pretas

        PlaceNewPiece(8, 'a', new Tower(Tabuleiro, Color.Preto));
        PlaceNewPiece(8, 'b', new Knight(Tabuleiro, Color.Preto));
        PlaceNewPiece(8, 'c', new Bishop(Tabuleiro, Color.Preto));
        PlaceNewPiece(8, 'd', new Queen(Tabuleiro, Color.Preto));
        PlaceNewPiece(8, 'e', new King(Tabuleiro, Color.Preto, this));
        PlaceNewPiece(8, 'f', new Bishop(Tabuleiro, Color.Preto));
        PlaceNewPiece(8, 'g', new Knight(Tabuleiro, Color.Preto));
        PlaceNewPiece(8, 'h', new Tower(Tabuleiro, Color.Preto));
        // Peões Pretos
        PlaceNewPiece(7, 'a', new Pawn(Tabuleiro, Color.Preto, this));
        PlaceNewPiece(7, 'b', new Pawn(Tabuleiro, Color.Preto, this));
        PlaceNewPiece(7, 'c', new Pawn(Tabuleiro, Color.Preto, this));
        PlaceNewPiece(7, 'd', new Pawn(Tabuleiro, Color.Preto, this));
        PlaceNewPiece(7, 'e', new Pawn(Tabuleiro, Color.Preto, this));
        PlaceNewPiece(7, 'f', new Pawn(Tabuleiro, Color.Preto, this));
        PlaceNewPiece(7, 'g', new Pawn(Tabuleiro, Color.Preto, this));
        PlaceNewPiece(7, 'h', new Pawn(Tabuleiro, Color.Preto, this));
    }

    private Color Adversary(Color color) => color == Color.Branco ? Color.Preto : Color.Branco;
}