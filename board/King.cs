using System.Configuration;
using System.Windows.Forms;

namespace ChessGame
{
    internal class King : Piece
    {
        static readonly string BLACK_KING = "\u265A";
        static readonly string WHITE_KING = "\u2654";

        public King(Position pos, bool isWhite) : base(pos, isWhite)
        {
            if (isWhite) { value = 11; }
            else { value = 12; }
            moved = false;
        }
        public override string Unicode()
        {
            if (IsWhite())
            {
                return WHITE_KING;
            }
            else
            { return BLACK_KING; }
        }
        public override List<Position> NextPositions(BoardState boardstate) // every position one square around the King/ and catsle next positions
        {
            List<Position> result = KingMoves(boardstate, GetPosition(), IsWhite());
            result.AddRange(CastleMoves(boardstate));
            return result;

        }
        private List<Position> CastleMoves(BoardState boardState) //adds the positions 2 spaces either side pf the king as a nextposition, if a castle is elligidible
        {
            List<Position> move = new List<Position>();
            Position kingPosition = GetPosition();

            int rookValue = 3;
            bool isPiece = false;

            if (!IsWhite())
            {
                rookValue = 4;
            }
            
            if (!moved)
            {
                List<Position> rooks = boardState.GetPosOfPiece(rookValue);
                foreach(Position rookpos in rooks)
                {
                   Piece rook = boardState.GetPieceAt(rookpos);
                    if (rook != null && !rook.moved)
                    {
                        
                        if(kingPosition.AddX(3).GetX() == rookpos.GetX() && kingPosition.GetY() == rookpos.GetY())
                        { 
                            for(int i = 1;i<3; i++)
                            {
                                isPiece = false;
                                Position checkPos = kingPosition.AddX(i);
                                Piece piece = boardState.GetPieceAt(checkPos);
                                if(piece != null)
                                {
                                    isPiece = true;
                                    break;                                  
                                }
                            }
                            if (!isPiece && ValidateCastle(boardState, kingPosition, 1))
                            {
                                move.Add(kingPosition.AddX(2));
                            }
                        }
                        if (kingPosition.AddX(-4).GetX() == rookpos.GetX() && kingPosition.GetY() == rookpos.GetY())
                        {
                            for (int i = -1; i > -4; i--)
                            {
                                isPiece = false;
                                Position checkPos = kingPosition.AddX(i);
                                Piece piece = boardState.GetPieceAt(checkPos);
                                if (piece != null)
                                {
                                    isPiece = true;
                                    break;
                                }
                            }
                            if (!isPiece && ValidateCastle(boardState, kingPosition, 2))
                            {
                                move.Add(kingPosition.AddX(-2));
                            }
                        }

                    }
                }
            }
            if(move == null)
            {
                move.Add(new Position(-1, -1));
            }
            return move;
        }

        private bool ValidateCastle(BoardState boardState, Position current, int direction) // checks if king is in check or if it moves trhough a check for the castle
        {
            if(InCheck(boardState, this.IsWhite()))
            {
                return false;
            }
            int num = 1;
            if (direction == 2)
            {
                num = -1;
            }
            boardState.RemoveAt(current);
            boardState.RemoveAt(current.AddX(num));
            boardState.AddAt(current.AddX(num), this);
            if (InCheck(boardState, this.IsWhite()))
            {
                boardState.RemoveAt(current.AddX(num));
                boardState.AddAt(current, this);
                return false;
            }
            boardState.RemoveAt(current.AddX(num));
            boardState.AddAt(current, this);
            return true;
        } 
    }
}