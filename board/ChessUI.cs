using System.CodeDom;

namespace ChessGame
{
    public partial class ChessUI : Form
    {
        private BoardState boardState = new BoardState();
        private readonly Dictionary<Position, Button> allButtons = new Dictionary<Position, Button>(); // Dictionary 
        private ChessClient chessClient;
        private List<Position> nextpositions;
        private Piece pieceToMove;
        public bool turn = true;
        public bool multiplayer;
        public bool player;
        public ChessUI(ChessClient chessClient)
        {
            InitializeComponent();
            this.chessClient = chessClient;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            btnMenu.Hide();
            BuildBoard();
            if (multiplayer)
            {
                if (player != turn)
                {
                    turn = player;
                    FlipBoard();
                    DisableButtons();
                }
                btn_flip.Hide();
            }
        }

        public void BuildBoard() // method to make a 8x8 board of buttons
        {
            const int constant = 150;
            for (int row = 0; row < 8; row++)
            {
                for (int col = 0; col < 8; col++)
                {
                    Position pos = new Position(row, col);
                    Button square = new Button()
                    {
                        Height = constant,
                        Width = constant,
                        Location = new Point((row * constant) + 200, (col * constant) + 200),
                        BackColor = (pos.GetY() + pos.GetX()) % 2 == 0 ? Color.White : Color.LightGray,
                        Font = new Font("Arial", 40, FontStyle.Bold),
                        Tag = pos,
                    };
                    Controls.Add(square);
                    allButtons.Add(pos, square);
                    square.Click += Square_Click; //method for when square is clicked

                }
            }
            EnableButtons(turn);
            DrawBoardState(boardState);
        }

        private void DefaultBackColor() // method to reset the colour of the board
        {
            foreach (KeyValuePair<Position, Button> entry in allButtons) //reseting colour of each square 
            {
                Button b = entry.Value;
                Position p = entry.Key;
                b.BackColor = (p.GetY() + p.GetX()) % 2 == 0 ? Color.White : Color.LightGray;
            }
        }

        private void Square_Click(object? sender, EventArgs e)
        {
            EnableButtons(turn);
            DefaultBackColor();
            bool moved = false;
            Button sendingButton = (Button)sender; //getting button clicked
            Position pos = (Position)sendingButton.Tag; //getting position of button clicked
            Piece piece = boardState.GetPieceAt(pos); //trying to get piece on button clicked


            if (piece != null && piece.IsWhite() == turn) //if clicked on a piece highlight it
            {
                sendingButton.BackColor = Color.Aquamarine;
            }

            if (pieceToMove != null) //if a piece was clicked on last time try to move it to the square clicked
            {
                moved = pieceToMove.MovePiece(pos, boardState, allButtons);
                if (moved) //if piece was successful in moving update current position of the piece, set the piece waiting to move to null, clear list of next positions and draw the new boardstate onto the buttons
                {
                    bool gameOver = boardState.InCheckMate(pieceToMove.IsWhite());
                    if (gameOver)
                    {
                        btnMenu.Show();
                        btn_flip.Enabled = false;
                        btnResign.Enabled = false;
                        DrawBoardState(boardState);
                        if (pieceToMove.InCheck(boardState, !pieceToMove.IsWhite()))
                        {
                           Finishgame(multiplayer, turn, false);
                        }
                        else
                        {
                            StaleMate();
                        }
                    }
                    else
                    {
                        if (!multiplayer)
                        {
                            if (turn)
                            {
                                turn = false;
                            }
                            else
                            {
                                turn = true;
                            }
                        }
                        DrawBoardState(boardState);
                        EnableButtons(turn);
                        pieceToMove = null;
                        nextpositions.Clear();
                    }

                }
            }
            if (piece != null && !moved && piece.IsWhite() == turn) //if no piece has succesfully moved, and hae clicked on a square with a piece set this to the new piece to move and find all of its next positions and add to the list 
            {
                pieceToMove = piece;
                nextpositions = pieceToMove.NextPositions(boardState);
                EnableNextPosButtons(nextpositions);
            }
            if (piece == null && !moved) //if no piece was clicked adn failed to move a piece set the piece waiting to move to null
            {
                pieceToMove = null;
            }
            if (multiplayer && moved)
            {
                chessClient.SendMove(boardState);
                DisableButtons();
            }
        }

        private void FlipBoard()
        {
            //for converting a dictionary to 2 lists from stack overflow and vice versa https://stackoverflow.com/questions/4038978/map-two-lists-into-a-dictionary-in-c-sharp
            List<Position> keys = allButtons.Select(kvp => kvp.Key).ToList();
            List<Button> Values = allButtons.Select(kvp => kvp.Value).ToList();
            Values.Reverse();
            for (int i = 0; i < keys.Count; i++)
            {
                allButtons[keys[i]] = Values[i];
            }
            foreach (KeyValuePair<Position, Button> entry in allButtons)
            {
                Position p = entry.Key;
                Button b = entry.Value;
                b.Tag = p;
            }
            EnableButtons(turn);
            DefaultBackColor();
            DrawBoardState(boardState);


        }

        private void DrawBoardState(BoardState boardState) //displaying all pieces on corresponding buttons
        {
            foreach (KeyValuePair<Position, Button> entry in allButtons)
            {
                Position pos = entry.Key;
                Button button = entry.Value;
                Piece picece = boardState.GetPieceAt(pos);
                if (picece != null)
                {
                    button.Text = picece.Unicode();
                }
                else
                {
                    button.Text = null;
                }
            }
        }

        private void EnableButtons(bool turn) //enabling the buttons of the players pieces each turn
        {
            foreach (KeyValuePair<Position, Button> entry in allButtons)
            {
                Position pos = entry.Key;
                Button button = entry.Value;
                Piece piece = boardState.GetPieceAt(pos);
                if (piece != null)
                {
                    if (piece.IsWhite() != turn)
                    {
                        button.Enabled = false;
                    }
                    else if (piece.IsWhite() == turn)
                    {
                        button.Enabled = true;
                    }
                }
            }
        }

        private void EnableNextPosButtons(List<Position> nextpositions) //enabling buttons of a pieces next positions
        {
            foreach (KeyValuePair<Position, Button> entry in allButtons)
            {
                Position pos = entry.Key;
                Button button = entry.Value;
                if (nextpositions != null && nextpositions.Contains(pos))
                {
                    button.Enabled = true;
                }
            }
        }

        private void Btn_flip_Click(object sender, EventArgs e) //when btn_flip is clicked run this
        {
            if (!multiplayer)
            {
                FlipBoard();
            }
        }

        private void BtnResign_Click(object sender, EventArgs e) //ending game if player surrenders
        {       
            if (multiplayer)
            {
                Finishgame(multiplayer, false, true);
                chessClient.Resign();
            }
            else
            {
                Finishgame(multiplayer, !turn, true);
            }
        }

        private void DisableButtons() // disables every button on the chessboard
        {
            foreach (KeyValuePair<Position, Button> entry in allButtons)
            {
                Button button = entry.Value;
                button.Enabled = false;
            }
        }

        private void BtnMenu_Click(object sender, EventArgs e) //returns user back to the Main Menu
        {
            ChessMenu menu = new ChessMenu();
            menu.Show();
            this.Close();
        }

        internal void SetBoardState(BoardState boardState) //Updates the boardstate for online games when receiving opponents move
        {
            this.boardState = boardState;
            DrawBoardState(this.boardState);
            if (boardState.InCheckMate(!player))
            {
                Piece pieceToCheck = new Pawn(new Position(0, 0), true); //a new piece just so I can test if the king is in check
                if (pieceToCheck.InCheck(boardState, !player))
                {
                    StaleMate();
                }
                else
                {
                    Finishgame(multiplayer, false, false);
                    btnMenu.Show();
                }
            }
            else
            {
                EnableButtons(turn);
            }
        }
        internal void ResignGame() // Called to show player that his opponent resigned, if online game
        {
            Finishgame(multiplayer, true, true);
        }
        private void StaleMate() //displays the game was a stale mate, and disables all buttons and shows the back to menu button
        {
            btnMenu.Show();
            btnResign.Enabled = false;
            btn_flip.Enabled = false;
            DisableButtons();
            labelResult.Text = "Stale mate, game is a draw";
        }
        private void Finishgame(bool multiplayer, bool win, bool resign) //The code for when a game finishes with a winner, displays outcome of game and disables all buttons and shows back to menu button
        {
            btnMenu.Show();
            btnResign.Enabled = false;
            btn_flip.Enabled = false;
            DisableButtons();
            if (multiplayer)
            {
                if (win)
                {
                    if (resign)
                    {
                        labelResult.Text = "Opponent resigned, you win!";
                    }
                    else
                    {
                        labelResult.Text = "Opponent in check mate, you win!";
                    }
                }
                else
                {
                    if (resign)
                    {
                        labelResult.Text = "You resigned, opponent wins";
                    }
                    else
                    {
                        labelResult.Text = "You are in check mate, opponent wins";
                    }
                }
            }
            else
            {
                if (win) //in terms of white team
                {
                    if (resign)
                    {
                        labelResult.Text = "Black resigned, white wins!";
                    }
                    else
                    {
                        labelResult.Text = "Black is in check mate, white wins!";
                    }
                }
                else
                {
                    if (resign)
                    {
                        labelResult.Text = "White resigned, black wins!";
                    }
                    else
                    {
                        labelResult.Text = "White in check mate, black wins!";
                    }
                }
            }
        }
    }
}
