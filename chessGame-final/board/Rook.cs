namespace ChessGame
{
    internal class Rook : Piece
    {
        static readonly string BLACK_ROOK = "\u265C"; //unicode for a black rook
        static readonly string WHITE_ROOK = "\u2656"; //unicode for a white rook
        public Rook(Position pos, bool isWhite) : base(pos, isWhite)
        {
            if (isWhite) { value = 3; }
            else { value = 4; }
            moved = false;
        }
        public override string Unicode() //return the unicode required
        {
            if (IsWhite())
            {
                return WHITE_ROOK;
            }
            else
            {
                return BLACK_ROOK;
            }
        }

        public override List<Position> NextPositions(BoardState boardState)
        {
            List<Position> result = new List<Position>();
            List<Position> horizontal = Horizontalmoves(boardState, GetPosition(), IsWhite());
            List<Position> vertical = Verticalmoves(boardState, GetPosition(), IsWhite());
            result.AddRange(horizontal);
            result.AddRange(vertical);
            return result;
            
        }
    }
}