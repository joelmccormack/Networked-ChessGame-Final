namespace ChessGame
{
    public abstract class Piece
    {
        private readonly bool white;
        public Position currentposition;
        public int value;
        public bool moved;
        public Piece(Position pos, bool isWhite)
        {
            this.currentposition = pos;
            this.white = isWhite;
        }

        public BoardState BoardState
        {
            get => default;
            set
            {
            }
        }

        public Position GetPosition() //returns current position
        {
            return this.currentposition;
        }
    
        public bool IsWhite() //returns this piece'scolour as boolean, true = white, false = black
        {
            return this.white;
        }

        protected static List<Position> Horizontalmoves(BoardState boardstate, Position myPosition, bool colour) // check all spaces to left and right of piece
        {
            List<Position> result = new List<Position>();

            for (int i = myPosition.GetX() + 1; i < 8; i++)
            {
                Position nextPos = new Position(i, myPosition.GetY()); // check all spaces going left
                Piece piece = boardstate.GetPieceAt(nextPos);
                if (piece == null)
                {
                    result.Add(nextPos);
                }
                else if (piece.IsWhite() != colour)
                {
                    result.Add(nextPos);
                    break;
                }
                else
                {
                    break;
                }
            }
            for (int i = myPosition.GetX() - 1; i >= 0; i--) // check all spaces going right
            {
                Position nextPos = new Position(i, myPosition.GetY());
                Piece piece = boardstate.GetPieceAt(nextPos);
                if (piece == null)
                {
                    result.Add(nextPos);
                }
                else if (piece.IsWhite() != colour)
                {
                    result.Add(nextPos);
                    break;
                }
                else
                {
                    break;
                }
            }
            return result;
        }

        protected static List<Position> Verticalmoves(BoardState boardstate, Position myPosition, bool colour) // check all spaces above and below piece
        {
            List<Position> result = new List<Position>();
            
            for (int i = myPosition.GetY() + 1; i < 8; i++) // check all spaces going down
            {
                Position nextPos = new Position(myPosition.GetX(), i);
                Piece piece = boardstate.GetPieceAt(nextPos);
                if (piece == null)
                {
                    result.Add(nextPos);
                }
                else if (piece.IsWhite() != colour)
                {
                    result.Add(nextPos);
                    break;
                }
                else
                {
                    break;
                }
            }
            for (int i = myPosition.GetY() - 1; i >= 0; i--) // check all spaces going up
            {
                Position nextPos = new Position(myPosition.GetX(), i);
                Piece piece = boardstate.GetPieceAt(nextPos);
                if (piece == null)
                {
                    result.Add(nextPos);
                }
                else if (piece.IsWhite() != colour)
                {
                    result.Add(nextPos);
                    break;
                }
                else
                {
                    break;
                }
            }
            return result;
        }

        protected static List<Position> Diagonalmove(BoardState boardstate, Position myPosition, bool colour) //check all spaces in all diagonal directions
        {
            List<Position> result = new List<Position>();
           
            for (int i = myPosition.GetY() + 1, j = myPosition.GetX() + 1; i < 8 && j < 8; i++, j++)
            {
                Position nextPos = new Position(j, i);
                Piece piece = boardstate.GetPieceAt(nextPos);
                if (piece == null)
                {
                    result.Add(nextPos);

                }
                else if (piece.IsWhite() != colour)
                {
                    result.Add(nextPos);
                    break;
                }
                else
                {
                    break;
                }

            }
            for (int i = myPosition.GetY() + 1, j = myPosition.GetX() - 1; i < 8 && j >= 0; i++, j--)
            {
                Position nextPos = new Position(j, i);
                Piece piece = boardstate.GetPieceAt(nextPos);
                if (piece == null)
                {
                    result.Add(nextPos);

                }
                else if (piece.IsWhite() != colour)
                {
                    result.Add(nextPos);
                    break;
                }
                else
                {
                    break;
                }
            }
            for (int i = myPosition.GetY() - 1, j = myPosition.GetX() + 1; i >= 0 && j < 8; i--, j++)
            {
                Position nextPos = new Position(j, i);
                Piece piece = boardstate.GetPieceAt(nextPos);
                if (piece == null)
                {
                    result.Add(nextPos);

                }
                else if (piece.IsWhite() != colour)
                {
                    result.Add(nextPos);
                    break;
                }
                else
                {
                    break;
                }
            }
            for (int i = myPosition.GetY() - 1, j = myPosition.GetX() - 1; i >= 0 && j >= 0; i--, j--)
            {
                Position nextPos = new Position(j, i);
                Piece piece = boardstate.GetPieceAt(nextPos);
                if (piece == null)
                {
                    result.Add(nextPos);

                }
                else if (piece.IsWhite() != colour)
                {
                    result.Add(nextPos);
                    break;
                }
                else
                {
                    break;
                }
            }
            return result;
        }

        protected static List<Position> KnightMoves(BoardState boardstate, Position myPosition, bool colour)//find all of the knights next positions 
        {
            List<Position> result = new List<Position>();
            Position nextPos = myPosition.AddY(-2).AddX(-1);
            CheckForPiece(nextPos, boardstate, result, colour);
            nextPos = myPosition.AddY(-1).AddX(-2);
            CheckForPiece(nextPos, boardstate, result, colour);
            nextPos = myPosition.AddY(1).AddX(-2);
            CheckForPiece(nextPos, boardstate, result, colour);
            nextPos = myPosition.AddY(2).AddX(-1);
            CheckForPiece(nextPos, boardstate, result, colour);
            nextPos = myPosition.AddY(2).AddX(1);
            CheckForPiece(nextPos, boardstate, result, colour);
            nextPos = myPosition.AddY(1).AddX(2);
            CheckForPiece(nextPos, boardstate, result, colour);
            nextPos = myPosition.AddY(-1).AddX(2);
            CheckForPiece(nextPos, boardstate, result, colour);
            nextPos = myPosition.AddY(-2).AddX(1);
            CheckForPiece(nextPos, boardstate, result, colour);
            return result;
        }
        
        protected List<Position> KingMoves(BoardState boardstate, Position myPosition, bool isWhite)//find all of the Kings next positions
        {
            List<Position> result = new List<Position>();
            Position nextPos = myPosition.AddY(-1);
            CheckForPiece(nextPos, boardstate, result, isWhite);
            nextPos = myPosition.AddY(-1).AddX(-1);
            CheckForPiece(nextPos, boardstate, result, isWhite);
            nextPos = myPosition.AddX(-1);
            CheckForPiece(nextPos, boardstate, result, isWhite);
            nextPos = myPosition.AddY(1).AddX(-1);
            CheckForPiece(nextPos, boardstate, result, isWhite);
            nextPos = myPosition.AddY(1);
            CheckForPiece(nextPos, boardstate, result, isWhite);
            nextPos = myPosition.AddY(1).AddX(1);
            CheckForPiece(nextPos, boardstate, result, isWhite);
            nextPos = myPosition.AddX(1);
            CheckForPiece(nextPos, boardstate, result, isWhite);
            nextPos = myPosition.AddY(-1).AddX(1);
            CheckForPiece(nextPos, boardstate, result, isWhite);
            return result;
        }
        
        private static void CheckForPiece(Position position, BoardState boardstate, List<Position> result, bool isWhite) //check that nextPos is allowed
        {

            if (position.GetX() >= 0 && position.GetX() <= 7 && position.GetY() >= 0 && position.GetY() <= 7) //check the nextPos is inside the board parameters
            {
                Piece piece = boardstate.GetPieceAt(position);
                if (piece == null || piece.IsWhite() != isWhite)
                {
                    result.Add(position);
                }

            }

        }
       
        public abstract string Unicode(); //the method each inherited class uses to return the unicode text of that piece

        public abstract List<Position> NextPositions(BoardState boardstate); //create a list of every legal next position for each piece

        public bool MovePiece(Position position, BoardState boardstate, Dictionary<Position, Button> allButtons)//to try and move a piece to square clicked
        {
           
            //get position of the square clicked last time. So where the piece is, before being moved
            Position currentPosition = GetPosition(); 
            
            //get the piece at currentPosition
            Piece pieceAtCurrent = boardstate.GetPieceAt(currentposition);

            //get colour of piece, true = white, false = black
            bool colour = pieceAtCurrent.IsWhite();
            

            //try to get piece at position other piece is trying to move to
            Piece pieceAtNext = boardstate.GetPieceAt(position);

            if (Valid(position, boardstate))
            {
                bool castledRight = false;
                bool castledLeft = false;   
                //move the piece
                if((pieceAtCurrent.value == 11 || pieceAtCurrent.value == 12) && (currentposition.AddX(2).GetX() == position.GetX()))
                {
                    Castlemove(1, currentposition, position, boardstate);
                    castledRight = true;
                }
                if ((pieceAtCurrent.value == 11 || pieceAtCurrent.value == 12) && (currentposition.AddX(-2).GetX() == position.GetX()))
                {
                    Castlemove(2, currentposition, position, boardstate);
                    castledLeft = true;
                }
                else
                {
                    boardstate.RemoveAt(currentPosition);
                    boardstate.RemoveAt(position);
                    boardstate.AddAt(position, this);
                }

                // if not in check then piece has succesfully been moved and return true
                if (!InCheck(boardstate, colour))
                {
                    pieceAtCurrent.moved = true;
                    pieceAtCurrent.currentposition = position;
                    if (pieceAtCurrent.value == 1 || pieceAtCurrent.value == 2)
                    {
                        PromotePawn(boardstate, pieceAtCurrent);
                    }

                    return true;
                    
                }
                //if in check then reverse move
                if (castledRight)
                {
                    ReverseCastle(1, currentPosition, position, boardstate);
                }
                else if (castledLeft)
                {
                    ReverseCastle(2,currentPosition, position, boardstate);
                }
                else
                {
                    boardstate.RemoveAt(position);
                    boardstate.AddAt(currentPosition, this);
                    boardstate.AddAt(position, pieceAtNext);
                }
                int i = 0;
                if (!colour)
                {
                    i = 1;
                }
                List<Position> kingPos = boardstate.GetPosOfPiece(11 + i);
                foreach(KeyValuePair<Position, Button> entry in allButtons)
                {
                    Position p = entry.Key;
                    Button b = entry.Value;
                    if(kingPos.FirstOrDefault().GetString() == p.GetString())
                    {
                        b.BackColor = Color.Red;
                    }
                }
            }
            //if not valid or in check then return false as piece is unable to move to the chosen square
            return false;
            
        }

        private static void PromotePawn(BoardState boardstate, Piece pawn)
        {
                Position pos = pawn.GetPosition();
                if (pos.GetY() == 0)
                {
                    boardstate.RemoveAt(pos);
                    boardstate.AddAt(pos, new Queen(pos, true));
                }
                
                if (pos.GetY() == 7)
                {
                    boardstate.RemoveAt(pos);
                    boardstate.AddAt(pos, new Queen(pos, false));
                }
        }

        private bool Valid(Position position, BoardState boardstate) //to ensure move being made is valid
        {
            List<Position> valid = NextPositions(boardstate);
            if(valid.Contains(position))
            {
                return true;
            }
            else
            { 
                return false;
            }

        } 
        
        public bool InCheck(BoardState boardstate, bool isWhite)//checks if the king of the piece trying to move is theatened by another piece
        {
            //get colour of piece just moved, check the king of that colour to see if any piece is threateninng ir and if so return true
            int i = 0;
            int j = 1;
            if (!isWhite)
            {
                i = 1;
                j = 0;
            }//Position of white King
            List<Position> KingList = boardstate.GetPosOfPiece(11 + i);
            Position kingPos = KingList.FirstOrDefault();

            //List of possible positions king could be threatended from
            List<Position> kingMove = KingMoves(boardstate, kingPos, isWhite);
            List<Position> knightMove = KnightMoves(boardstate, kingPos, isWhite);
            List<Position> diagonal = Diagonalmove(boardstate, kingPos, isWhite);
            List<Position> horizontalAndVertical = Horizontalmoves(boardstate, kingPos, isWhite);
            horizontalAndVertical.AddRange(Verticalmoves(boardstate, kingPos, isWhite));

            //List of positions of where the black pieces are
            List<Position> oppnentKingList = boardstate.GetPosOfPiece(11 + j);
            List<Position> opponentKnightList = boardstate.GetPosOfPiece(5 + j);
            List<Position> opponentBishList = boardstate.GetPosOfPiece(7 + j);
            List<Position> opponentRookList = boardstate.GetPosOfPiece(3 + j);

            //positions from the list of positions of the black pieces
            Position opponentKingPos = oppnentKingList.FirstOrDefault();
            Position opponentRookPos1 = opponentRookList.FirstOrDefault();
            Position opponentRookPos2 = opponentRookList.Skip(1).FirstOrDefault();
            if (opponentRookList.Count() < 2)
            {
                opponentRookPos2 = new Position(-1, -1);
            }
            Position opponentBishPos1 = opponentBishList.FirstOrDefault();
            Position opponentBishPos2 = opponentBishList.Skip(1).FirstOrDefault();
            if (opponentBishList.Count() < 2)
            { 
                opponentBishPos2 = new Position(-1, -1);
            }
            Position opponentKnightPos1 = opponentKnightList.FirstOrDefault();
            Position opponentKnightPos2 = opponentKnightList.Skip(1).FirstOrDefault();
            if (opponentKnightList.Count() < 2)
            {
                opponentKnightPos2 = new Position(-1, -1);
            }

            //checking if the positions of the black pieces are in the list of the threatening positions for each piece              
            if (diagonal.Contains(opponentBishPos1) || diagonal.Contains(opponentBishPos2) || knightMove.Contains(opponentKnightPos1)
                || knightMove.Contains(opponentKnightPos2) || kingMove.Contains(opponentKingPos) || horizontalAndVertical.Contains(opponentRookPos1)
                || horizontalAndVertical.Contains(opponentRookPos2))
            {
                return true;
            }
            if (isWhite)
            {
                List<Position> blackPawnsList = boardstate.GetPosOfPiece(2);
                List<Position> blackQueenList = boardstate.GetPosOfPiece(10);

                // checking for black pawns
                foreach (Position position in blackPawnsList) 
                {
                    Position potentialPawn = kingPos.AddX(1).AddY(-1);
                    Position potentialPawn2 = kingPos.AddX(-1).AddY(-1);
                    if (diagonal.Contains(position) && ((position.GetX() == potentialPawn.GetX() && position.GetY() == potentialPawn.GetY()) || position.GetX() == potentialPawn2.GetX() && position.GetY() == potentialPawn2.GetY()))
                    {
                        return true;
                    }               
                }
                foreach(Position pos in blackQueenList)
                {
                    if(diagonal.Contains(pos) || horizontalAndVertical.Contains(pos))
                    {
                        return true;
                    }
                }
            }
            else
            {
                // cheking for white pawns
                List<Position> whitePawnsList = boardstate.GetPosOfPiece(1);
                List<Position> whiteQueenList = boardstate.GetPosOfPiece(9);

                foreach (Position position in whitePawnsList)
                {
                    Position potentialPawn = kingPos.AddX(1).AddY(1);
                    Position potentialPawn2 = kingPos.AddX(-1).AddY(1);
                    if (diagonal.Contains(position) && ((position.GetX() == potentialPawn.GetX() && position.GetY() == potentialPawn.GetY()) || position.GetX() == potentialPawn2.GetX() && position.GetY() == potentialPawn2.GetY()))
                    {
                        return true;
                    }
                }
                foreach(Position pos in whiteQueenList)
                {
                    if(diagonal.Contains(pos) || horizontalAndVertical.Contains(pos))
                    {
                        return true;
                    }
                }
            }

            return false;
        
        }

        private void Castlemove(int direction, Position kingPos, Position kingNextPos, BoardState boardstate ) //performs the castle move where king moves two spaces towards rook and the rook moves to the otherside of the king
        {
            if (direction == 1) //short castle
            {
                Position rookPos = kingPos.AddX(3);
                Piece rook = boardstate.GetPieceAt(rookPos);
                boardstate.RemoveAt(kingPos);
                boardstate.RemoveAt(rookPos);
                boardstate.RemoveAt(kingNextPos);
                boardstate.AddAt(kingNextPos.AddX(-1), rook);               
                boardstate.AddAt(kingNextPos, this);
                rook.currentposition = kingNextPos.AddX(-1);
            }
            if(direction == 2) //long castle
            {
                Position rookPos = kingPos.AddX(-4);
                Piece rook = boardstate.GetPieceAt(rookPos);
                boardstate.RemoveAt(kingPos);
                boardstate.RemoveAt(rookPos);
                boardstate.RemoveAt(kingNextPos);
                boardstate.AddAt(kingNextPos.AddX(1), rook);
                boardstate.AddAt(kingNextPos, this);
                rook.currentposition = kingNextPos.AddX(1);
            }
        }
       
        private void ReverseCastle(int direction,Position kingLastPos, Position kingNewPosition, BoardState boardstate) //reverses the castle move just made
        {
            if (direction == 1)
            {
                Piece rook = boardstate.GetPieceAt(kingNewPosition.AddX(-1));
                boardstate.RemoveAt(kingNewPosition.AddX(-1));
                boardstate.RemoveAt(kingNewPosition);
                boardstate.AddAt(kingLastPos, this);
                boardstate.AddAt(kingLastPos.AddX(3), rook);
                rook.currentposition = kingLastPos.AddX(3);
            }
            if (direction == 2)
            {
                Piece rook = boardstate.GetPieceAt(kingNewPosition.AddX(1));
                boardstate.RemoveAt(kingNewPosition.AddX(1));
                boardstate.RemoveAt(kingNewPosition);
                boardstate.AddAt(kingLastPos, this);
                boardstate.AddAt(kingLastPos.AddX(-4), rook);
                rook.currentposition = kingLastPos.AddX(-4);
            }
        }
    }
}