namespace ChessGame
{
    internal class Queen : Piece
    {
        static readonly string BLACK_QUEEN = "\u265B";
        static readonly string WHITE_QUEEN = "\u2655";
        public Queen(Position pos, bool isWhite) : base(pos, isWhite)
        {
            if (isWhite) { value = 9; }
            else { value = 10; }
            moved = false;
        }
        public override string Unicode()
        {
            if (IsWhite())
            {
                return WHITE_QUEEN;
            }
            else
            { return BLACK_QUEEN; }
        }
        public override List<Position> NextPositions(BoardState boardstate)
        {
            List<Position> result = new List<Position>();
            List<Position> diagonal = Diagonalmove(boardstate, GetPosition(), IsWhite());
            List<Position> horizontal = Horizontalmoves(boardstate, GetPosition(), IsWhite());
            List<Position> vertical = Verticalmoves(boardstate, GetPosition(), IsWhite());
            result.AddRange(diagonal);
            result.AddRange(horizontal);
            result.AddRange(vertical);
            return result;
        }
    }
}