using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessGame
{
    public partial class ChessMenu : Form
    {
        ChessClient chessClient = new ChessClient();
        ChessUI currentgame;
        private string recentGameName;
        public ChessMenu() //initializes all components, and sets timer intervals
        {
            InitializeComponent();
            TimerConnect.Interval = 1000;
            TimerConnect.Tick += TimerConnect_Tick;
            TimerConnect.Start();
            TimerCheckEvents.Interval = 100;
            TimerCheckEvents.Tick += TimerCheckEvents_Tick;
            FormClosing += Form_FormClosing; //reference for executing code before closing form https://stackoverflow.com/questions/17608188/executing-code-when-my-form-is-closed          
        }

        private void ChessMenu_Load(object sender, EventArgs e) //displays chess menu when form loads
        {
            btnNewGame.Hide();
            btnJoinGame.Hide();
            TBServerName.Hide();
            btnEnter.Hide();
            gameIDGridView.Hide();
            btnReturn.Hide();
        }

        private void Form_FormClosing(object? sender, FormClosingEventArgs e) //tries to remove any game that was created when form closes
        {
            if (chessClient.serverConnected)
            {
                chessClient.TryDeleteGame(recentGameName);
            }
        }

        private void BtnOnline_Click(object sender, EventArgs e) //when online buttons is clicked show new the online menu, or show message box saying can't connect to server
        {
            if (chessClient.serverConnected)
            {
                btnOnline.Hide();
                btnPass.Hide();
                btnNewGame.Show();
                btnJoinGame.Show();
                TimerCheckEvents.Start();
                btnReturn.Show();
            }
            else
            {
                MessageBox.Show("Server not online or have not been able to connect");
            }
        }

        private void BtnPass_Click(object sender, EventArgs e) //start a pass and play game when clicked
        {
            this.Hide();
            ChessUI game = new(chessClient)
            {
                multiplayer = false
            };
            game.Show();
        }

        private void BtnNewGame_Click(object sender, EventArgs e) //allow user to create a new chess game, by chosing a anme for it
        {
            btnJoinGame.Hide();
            TBServerName.Show();
            btnNewGame.Enabled = false;
            btnEnter.Show();
        }

        private void BtnJoinGame_Click(object sender, EventArgs e) // displays data view grid containing all games so user can select which game they want to join 
        {
            btnNewGame.Hide();
            btnJoinGame.Enabled = false;
            gameIDGridView.Show();
            chessClient.JoinGame();
        }

        private void BtnEnter_Click(object sender, EventArgs e) //confirms the name is valid and then sends this to server to create
        {
            if (TBServerName.TextLength < 17 && TBServerName.TextLength > 3 && TBServerName.Text == TBServerName.Text.Replace(" ", string.Empty) && !TBServerName.Text.Contains(';') && !TBServerName.Text.Contains(',')) // referencing how to remove spaces from a string https://stackoverflow.com/questions/4798102/how-do-you-remove-the-space-in-a-c-sharp-string
            {
                btnEnter.Enabled = false;
                TBServerName.Enabled = false;
                string serverName = TBServerName.Text.Trim();
                chessClient.NewGame(serverName);
                recentGameName = serverName;
            }
            else
            {
                MessageBox.Show("Name can't have spaces, and has to be from 4-16 characters and cannot contain any commas or semi colons");
            }      
        }

        private void TimerConnect_Tick(object sender, EventArgs e) //every tick, tries connecting to server, when connected stop running
        {
            chessClient.Connected();
            if (chessClient.serverConnected)
            {
                TimerConnect.Stop();

            }
        }

        private void TimerCheckEvents_Tick(object? sender, EventArgs e) //every tick handles the incoming messages from the server
        {
            string data = chessClient.CheckServerEvents();
            if (data != null)
            {
                string[] strings = data.Split(";");
                switch (strings[0])
                {
                    case "playerJoined":
                        {
                            StartGame(true);
                            break;
                        }
                    case "gameid":
                        {
                            string[] games = strings[1].Split(",");
                            if (games.Count() == 0)
                            {
                                break;
                            }
                            foreach (string game in games)
                            {
                                string[] row = { game, "" };
                                this.gameIDGridView.Rows.Add(row);
                            }
                            break;
                        }
                    case "no-servers-available":
                        {
                            break;
                        }
                    case "makemove":
                        {
                            if (strings[1] == "resign")
                            {
                                this.currentgame.ResignGame();
                                break;
                            }
                            BoardState boardState = chessClient.StringToBoardState(data);
                            this.currentgame.SetBoardState(boardState);
                            break;
                        }
                    default: { break; }
                }
            }
        }

        private void StartGame(bool player) //Starts the chess game
        {
            this.Hide();
            currentgame = new(chessClient)
            {
                multiplayer = true,
                player = player
            };
            currentgame.Show();
        }

        private void BtnReturn_Click(object sender, EventArgs e) //returns back to main menu
        {
            btnNewGame.Hide();
            btnNewGame.Enabled = true;
            btnJoinGame.Hide();
            btnJoinGame.Enabled = true;
            btnEnter.Hide();
            btnEnter.Enabled = true;
            chessClient.TryDeleteGame(TBServerName.Text);
            TBServerName.Hide();
            TBServerName.Enabled = true;
            TBServerName.Clear();
            btnPass.Show();
            btnOnline.Show();
            gameIDGridView.Hide();
            btnReturn.Hide();
        }

        private void GameID_DoubleClick(object sender, EventArgs e) //tells server which game wants to join
        {
            DataGridView grid = sender as DataGridView;
            if (grid != null && grid.Rows.Count > 0 && grid.CurrentCell.RowIndex >= 0)
            {
                int rowIndex = grid.CurrentCell.RowIndex;
                DataGridViewRow row = grid.Rows[rowIndex];
                string gameID = Convert.ToString(row.Cells["gvGameID"].Value);
                chessClient.Join(gameID);
                StartGame(false);
            }
        }      
    }
}
    