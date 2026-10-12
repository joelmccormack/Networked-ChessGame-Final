namespace ChessGame
{
    internal class Bishop : Piece
    {
        static readonly string BLACK_BISHOP = "\u265D";
        static readonly string WHITE_BISHOP = "\u2657";
        public Bishop(Position pos, bool isWhite) : base(pos, isWhite)
        {
            if (isWhite) { value = 7; }
            else { value = 8; }
            moved = false;
        }
        public override string Unicode()
        {
            if (IsWhite())
            {
                return WHITE_BISHOP;
            }
            else 
            { return BLACK_BISHOP; }
        }
        public override List<Position> NextPositions(BoardState boardstate)
        {
            List<Position> result = new List<Position>();
            List<Position> diagonal = Diagonalmove(boardstate, GetPosition(), IsWhite());
            result.AddRange(diagonal);
            return result;
        } 
    }
}