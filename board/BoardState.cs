using System.Data.SqlTypes;
using System.Xml;

namespace ChessGame
{
    public class BoardState
    {
        public  Dictionary<Position, Piece> allPieces; 
        public BoardState()
        {
            allPieces = new Dictionary<Position, Piece>();
             AddPawnsAtRow(6, true); 
             AddPawnsAtRow(1, false);
             AddRooks();
             AddBishops();
             AddQueens();
             AddKings();
             AddKnights();                        
        }

        public Piece? GetPieceAt(Position pos) // method to get the piece at a specified position
        {
            return allPieces.GetValueOrDefault(pos, null);
        }

        private void AddKings() //method to add a black and white King in its starting position
        {
            Position pos = new Position(4, 0);
            allPieces.Add(pos, new King(pos, false));
            pos = new Position(4, 7);
            allPieces.Add(pos, new King(pos, true));
        }

        private void AddQueens() //method to add a black and white queen in its starting position
        {
            Position pos = new Position(3, 0);
            allPieces.Add(pos, new Queen(pos, false));
            pos = new Position(3, 7);
            allPieces.Add(pos, new Queen(pos, true));
        }

        private void AddKnights() //method to add the black and white knights in their starting positions
        {
            Position pos = new Position(1, 0);
            allPieces.Add(pos, new Knight(pos, false));
            pos = new Position(6, 0);
            allPieces.Add(pos, new Knight(pos, false));
            pos = new Position(1, 7);
            allPieces.Add(pos, new Knight(pos, true));
            pos = new Position(6, 7);
            allPieces.Add(pos, new Knight(pos, true));
        }

        private void AddBishops() //method to add the black and white bishops in their starting positions
        {
            Position pos = new Position(2, 0);
            allPieces.Add(pos, new Bishop(pos, false));
            pos = new Position(5, 0);
            allPieces.Add(pos, new Bishop(pos, false));
            pos = new Position(2, 7);
            allPieces.Add(pos, new Bishop(pos, true));
            pos = new Position(5, 7);
            allPieces.Add(pos, new Bishop(pos, true));
        }

        private void AddRooks() //method to add the black and white rooks in their starting positions
        {
            Position pos = new Position(0, 0);
            allPieces.Add(pos, new Rook(pos, false));
            pos = new Position(7, 0);
            allPieces.Add(pos, new Rook(pos, false));
            pos = new Position(0, 7);
            allPieces.Add(pos, new Rook(pos, true));
            pos = new Position(7, 7);
            allPieces.Add(pos, new Rook(pos, true));
        }

        private void AddPawnsAtRow(int row, bool isWhite) //method to add a row of pawns for either white or black  
        {
            Position pos = new Position(0, row);
            for (int i = 0; i < 8; i++)
            {
                allPieces.Add(pos, new Pawn(pos, isWhite));

                pos = pos.AddX(1);
            }
        }

        public void RemoveAt(Position currentPosition) //removes a Position and piece from the allPieces dictionary
        {
           allPieces.Remove(currentPosition);            
        }

        public void AddAt(Position position, Piece piece) //adds a new Position and Piece to allPieces
        {
            allPieces.Add(position, piece);
        }

        public List<Position> GetPosOfPiece(int value) //returns a List of Position's of a type of piece
        {
            List<Position> piecesPos = new List<Position>();
            foreach(KeyValuePair<Position, Piece> pair in allPieces)
            {
                Position position = pair.Key;
                Piece piece = pair.Value;
                if (piece != null)
                {
                    if (piece.value == value)
                    {
                        piecesPos.Add(position);
                    }
                }
            }
            if(piecesPos.Count() == 0)
            {
                piecesPos.Add(new Position(-1, -1));
            }
            return piecesPos;    
        }

        public bool InCheckMate(bool isWhite) //checks that their is a checkmate or stalemate
        {
            List<Position> allLegalMoves = GetValidMoves(isWhite);
            if (allLegalMoves.Count == 0)
            {
                return true;
            }
            return false;
        }

        private List<Position> GetValidMoves(bool isWhite) //returns a List of valid moves from any piece
        {
            List<Position> allValidNextPositions = new List<Position>();
            foreach(KeyValuePair<Position, Piece> entry in allPieces.ToList())
            {
                Position posOfPiece = entry.Key;
                Piece piece = entry.Value;
                if (piece != null)  
                {                  
                    if (piece.IsWhite() != isWhite)
                    {
                        List<Position> nextpositions = piece.NextPositions(this);
                        allValidNextPositions.AddRange(TestMove(nextpositions, posOfPiece, piece, isWhite));
                        if (allValidNextPositions.Count > 0)
                        {
                            return allValidNextPositions;
                        }
                    }
                }
            }         
            return allValidNextPositions;
        }
     
        private List<Position> TestMove(List<Position> nextPositions,Position posOfPiece, Piece piece, bool isWhite) //simulate the move of a piece to decide if its in checkmate
        {
            List<Position> AllLegalMoves = new List<Position>();
            foreach(Position pos in nextPositions)
            {
                Piece pieceAtNext = GetPieceAt(pos);
                RemoveAt(posOfPiece);
                RemoveAt(pos);
                AddAt(pos, piece);
                               
                if (!piece.InCheck(this, !isWhite))
                {
                    AllLegalMoves.Add(pos);
                    RemoveAt(pos);
                    AddAt(posOfPiece, piece);
                    AddAt(pos, pieceAtNext);
                    return AllLegalMoves; //only need to find one legal move so return as soon as found one
                }
                RemoveAt(pos);
                AddAt(posOfPiece, piece);
                AddAt(pos, pieceAtNext);
            }
            return AllLegalMoves;            
        }       
    }
}