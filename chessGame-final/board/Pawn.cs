namespace ChessGame
{
    internal class Pawn : Piece
    {
        static readonly string BLACK_PAWN = "\u265F";
        static readonly string WHITE_PAWN = "\u2659";
        public Pawn(Position pos, bool isWhite) : base(pos,  isWhite)
        {
            if (isWhite) { value = 1; }
            else { value = 2; }
            moved = false;
        }

        public override string Unicode()
        {
            if (IsWhite())
            {
                return WHITE_PAWN;
            }
            else
            {
                return BLACK_PAWN;
            }
        }
        public override List<Position> NextPositions(BoardState boardstate)
        {
            List<Position> result = new List<Position>();
            Position myPosition = GetPosition();
            if (IsWhite())
            {
                Position next = myPosition.AddY(-1);
                Piece piece = boardstate.GetPieceAt(next);
                if (piece == null)
                {
                    result.Add(next);
                    if (!moved)
                    {
                        next = myPosition.AddY(-2);
                        piece = boardstate.GetPieceAt(next);
                        if (piece == null)                    
                        {
                            result.Add(next);

                        }
                    }
                }
                if (myPosition.GetX() > 0)
                {
                    next = myPosition.AddY(-1).AddX(-1);
                    piece = boardstate.GetPieceAt(next);
                    if (piece != null)
                    {
                        if (piece.IsWhite() != this.IsWhite())
                        {
                            result.Add(next);
                        }
                    }
                }
                if (myPosition.GetX() < 7)
                {
                    next = myPosition.AddY(-1).AddX(1);
                    piece = boardstate.GetPieceAt(next);
                    if (piece != null)
                    {
                        if(piece.IsWhite() != this.IsWhite())
                        {
                            result.Add(next);
                        }            
                    }      
                }              

            }
            else
            {
                Position next = myPosition.AddY(1);
                Piece piece = boardstate.GetPieceAt(next);
                if (piece == null)
                {
                    result.Add(next);
                    if (!moved)
                    {
                        next = myPosition.AddY(2);
                        piece = boardstate.GetPieceAt(next);
                        if (piece == null)
                        {
                            result.Add(next);
                        }
                    }
                }
                if (myPosition.GetX() > 0)
                {
                    next = myPosition.AddY(1).AddX(-1);
                    piece = boardstate.GetPieceAt(next);
                    if (piece != null)
                    {
                        if (piece.IsWhite() != this.IsWhite())
                        {     
                            result.Add(next);
                        }
                    }
                }
                if (myPosition.GetX() < 7)
                {
                    next = myPosition.AddY(1).AddX(1);
                    piece = boardstate.GetPieceAt(next);
                    if (piece != null)
                    {
                        if (piece.IsWhite() != this.IsWhite())
                        {
                            result.Add(next);
                        }
                    }
                }
            }
            return result;
        }    
    }
}