namespace ChessGame
{
    internal class Knight : Piece
    {
        //unicode for black and white knight
        static readonly string BLACK_KNIGHT = "\u265E"; 
        static readonly string WHITE_KNIGHT = "\u2658";
        public Knight(Position pos, bool isWhite) : base(pos, isWhite)
        {
            if (isWhite) { value = 5; }
            else { value = 6; }
            moved = false;
        }
        public override string Unicode()
        {
            if (IsWhite())
            {
                return WHITE_KNIGHT;
            }
            else
            { return BLACK_KNIGHT; }
        }
        public override List<Position> NextPositions(BoardState boardstate)
        {

            List<Position> result = KnightMoves(boardstate, GetPosition(), IsWhite());            
            return result;

        }

        
    }
}