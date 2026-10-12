using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace ChessGame
{

    public class ChessClient
    {
    
        private readonly UdpClient udpClient;
        readonly IPEndPoint remoteIP = new IPEndPoint(IPAddress.Broadcast, 55555);
        private IPEndPoint serverIPEndPoint = new IPEndPoint(IPAddress.Any, 0);
        public bool serverConnected = false;
        
        public ChessClient()
        {
            udpClient = new UdpClient();
        }
        internal void Connected() //sends a broadcast message to any local IPAddress with the Port 55555, and then receives available data to confirm if the server has been found
        {
            if (serverConnected == false)
            {
                Byte[] bytes = ASCIIEncoding.ASCII.GetBytes("finding-server");
                udpClient.Send(bytes, bytes.Length, remoteIP);

                if (udpClient.Available > 0)
                {
                    string message = ASCIIEncoding.ASCII.GetString(udpClient.Receive(ref serverIPEndPoint));
                    if (message == "chess-server")
                    {
                        serverConnected = true;
                    }
                }
            }
        }

        internal void SendMove(BoardState boardState) //sends data to server, containing information about the most recent update to the chess game, so either a piece moved or player resigned
        {
            string data = (BoardstateToString(boardState));
            Byte[] bytes = Encoding.ASCII.GetBytes(data);
            udpClient.Send(bytes, bytes.Length, serverIPEndPoint);
        }

        public string BoardstateToString(BoardState boardstate) //Converts the Boardstate into a string so can be sent across network 
        {
            string board = "makemove;"; 
            Dictionary<Position, Piece> pieces = boardstate.allPieces;
            foreach (KeyValuePair<Position, Piece> entry in pieces)
            {
                Position pos = entry.Key;
                Piece piece = entry.Value;
                if (piece == null)
                {
                    continue;
                }
                int x = pos.GetX();
                int y = pos.GetY();
                int posValue = (y * 8) + x;
                string pieceValue = PieceToString(piece);
                board += ($"{posValue.ToString()}={pieceValue},");
            }
            board = board.Substring(0, board.Length-1);
            return board;
        }

        private string PieceToString(Piece piece) //Converts a piece into a character which represents itself as a string
        {
            string pieceValue = string.Empty;
            int value = piece.value;
            switch (value)
            {
                case (1):
                    {
                        pieceValue = "P";
                        break;
                    }    
                case (2):
                    {
                        pieceValue = "p";
                        break;
                    }
                case (3):
                    {
                        pieceValue = "R";
                        break;
                    }
                case (4):
                    {
                        pieceValue = "r";
                        break;
                    }
                case (5):
                    {
                        pieceValue = "H";
                        break;
                    }
                case (6):
                    {
                        pieceValue = "h";
                        break;
                    }
                case (7):
                    {
                        pieceValue = "B";
                        break;
                    }
                case (8):
                    {
                        pieceValue = "b";
                        break;
                    }
                case (9):
                    {
                        pieceValue = "Q";
                        break;
                    }
                case (10):
                    {
                        pieceValue = "q";
                        break;
                    }
                case (11):
                    {
                        pieceValue = "K";
                        break;
                    }
                case (12):
                    {
                        pieceValue = "k";
                        break;
                    }
                default: 
                    break;
            }
            return pieceValue;
        }

        public BoardState StringToBoardState(string board) //Converts a string representing the baordstate into a BoardState 
        {
            BoardState boardstate = new BoardState();
            boardstate.allPieces.Clear();
            string[] values = board.Split(";");
            string[] piecePos = values[1].Split(",");
            for (int i = 0; i < piecePos.Length; i++)
            {
                string[] pos = piecePos[i].Split("=");
                int num = Convert.ToInt32(pos[0]);
                int x = (num) % 8;
                int y = (num) / 8;
                Position position = new Position(x, y);
                Piece piece = ParsePiece(position, boardstate, pos[1]);
                boardstate.AddAt(position, piece);
            }
            return boardstate;
        }

        private Piece ParsePiece(Position position, BoardState boardstate, string piece) //Converts a character representing the piece into its piece
        {
            switch (piece)
            {
                case "P":
                    {
                        return new Pawn(position, true);
                    }
                case "p":
                    {
                        return new Pawn(position, false);

                    }
                case "R":
                    {
                        return new Rook(position, true);
                    }
                case "r":
                    {
                        return new Rook(position, false);
                    }
                case "H":
                    {
                        return new Knight(position, true);
                    }
                case "h":
                    {
                        return new Knight(position, false);
                    }
                case "B":
                    {
                        return new Bishop(position, true);
                    }
                case "b":
                    {
                        return new Bishop(position, false);
                    }
                case "Q":
                    {
                        return new Queen(position, true);
                    }
                case "q":
                    {
                        return new Queen(position, false);
                    }
                case "K":
                    {
                        return new King(position, true);
                    }
                case "k":
                    {
                        return new King(position, false);
                    }

                default:
                break;
            }
            return null;

               
        }

        internal void NewGame(string servername) //requests server to create new game with string servername
        {
            Byte[] bytes = Encoding.ASCII.GetBytes($"newgame;{servername}");
            udpClient.Send(bytes, bytes.Length, serverIPEndPoint);
        }
       
        internal void JoinGame() //requests server to return all of the joinable Games's gameIDs
        {
            Byte[] bytes = Encoding.ASCII.GetBytes("joingame");
            udpClient.Send(bytes, bytes.Length, serverIPEndPoint);          
        }

        internal void Resign() 
        {
            Byte[] bytes = Encoding.ASCII.GetBytes("makemove;resign"); //informs server that this player has resigned so it can update the other player
            udpClient.Send(bytes, bytes.Length, serverIPEndPoint);
        }

        internal void Join(string name) //requests server to add this player to the Game with gameID equal to name
        {
            string message = ($"addplayer;{name}");
            Byte[] bytes = Encoding.ASCII.GetBytes(message);
            udpClient.Send(bytes, bytes.Length, serverIPEndPoint);
        }
      
        internal string CheckServerEvents() //listens for incoming bytes from network
        {
            if (udpClient.Available > 0)
            {
                Byte[] bytes = udpClient.Receive(ref serverIPEndPoint);
                string data = Encoding.ASCII.GetString(bytes);
                return data;
            }
            return string.Empty;
        }

        internal void TryDeleteGame(string text) // requests server to delete the Game with gameID text
        {
            text = "deletegame;" + text;
            Byte[] bytes = Encoding.ASCII.GetBytes(text);
            udpClient.Send(bytes, bytes.Length, serverIPEndPoint);
        }
    }
}
