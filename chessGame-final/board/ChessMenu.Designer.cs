namespace ChessGame
{
    partial class ChessMenu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            btnPass = new Button();
            btnOnline = new Button();
            labelTitle = new Label();
            btnNewGame = new Button();
            btnJoinGame = new Button();
            TBServerName = new TextBox();
            btnEnter = new Button();
            TimerConnect = new System.Windows.Forms.Timer(components);
            TimerCheckEvents = new System.Windows.Forms.Timer(components);
            btnReturn = new Button();
            gvGameID = new DataGridViewTextBoxColumn();
            gameIDGridView = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)gameIDGridView).BeginInit();
            SuspendLayout();
            // 
            // btnPass
            // 
            btnPass.Location = new Point(1274, 774);
            btnPass.Margin = new Padding(4, 2, 4, 2);
            btnPass.Name = "btnPass";
            btnPass.Size = new Size(318, 81);
            btnPass.TabIndex = 0;
            btnPass.Text = "Pass And Play";
            btnPass.UseVisualStyleBackColor = true;
            btnPass.Click += BtnPass_Click;
            // 
            // btnOnline
            // 
            btnOnline.Location = new Point(1274, 882);
            btnOnline.Margin = new Padding(4, 2, 4, 2);
            btnOnline.Name = "btnOnline";
            btnOnline.Size = new Size(318, 81);
            btnOnline.TabIndex = 1;
            btnOnline.Text = "Online Multiplayer";
            btnOnline.UseVisualStyleBackColor = true;
            btnOnline.Click += BtnOnline_Click;
            // 
            // label1
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Segoe UI", 50F, FontStyle.Regular, GraphicsUnit.Point);
            labelTitle.Location = new Point(891, 489);
            labelTitle.Margin = new Padding(4, 0, 4, 0);
            labelTitle.Name = "label1";
            labelTitle.Size = new Size(1154, 177);
            labelTitle.TabIndex = 2;
            labelTitle.Text = "Joel's Chess Game";
            // 
            // btnNewGame
            // 
            btnNewGame.Location = new Point(1877, 821);
            btnNewGame.Margin = new Padding(4, 2, 4, 2);
            btnNewGame.Name = "btnNewGame";
            btnNewGame.Size = new Size(318, 81);
            btnNewGame.TabIndex = 3;
            btnNewGame.Text = "New Game";
            btnNewGame.UseVisualStyleBackColor = true;
            btnNewGame.Click += BtnNewGame_Click;
            // 
            // btnJoinGame
            // 
            btnJoinGame.Location = new Point(595, 823);
            btnJoinGame.Margin = new Padding(4, 2, 4, 2);
            btnJoinGame.Name = "btnJoinGame";
            btnJoinGame.Size = new Size(318, 79);
            btnJoinGame.TabIndex = 4;
            btnJoinGame.Text = "Join Game";
            btnJoinGame.UseVisualStyleBackColor = true;
            btnJoinGame.Click += BtnJoinGame_Click;
            // 
            // TBServerName
            // 
            TBServerName.Location = new Point(1877, 924);
            TBServerName.Margin = new Padding(4, 2, 4, 2);
            TBServerName.Name = "TBServerName";
            TBServerName.Size = new Size(320, 39);
            TBServerName.TabIndex = 5;
            // 
            // btnEnter
            // 
            btnEnter.Location = new Point(2220, 924);
            btnEnter.Margin = new Padding(4, 2, 4, 2);
            btnEnter.Name = "btnEnter";
            btnEnter.Size = new Size(150, 47);
            btnEnter.TabIndex = 6;
            btnEnter.Text = "Enter";
            btnEnter.UseVisualStyleBackColor = true;
            btnEnter.Click += BtnEnter_Click;
            // 
            // btnReturn
            // 
            btnReturn.Location = new Point(1274, 821);
            btnReturn.Margin = new Padding(4, 2, 4, 2);
            btnReturn.Name = "btnReturn";
            btnReturn.Size = new Size(318, 81);
            btnReturn.TabIndex = 7;
            btnReturn.Text = "Return to Menu";
            btnReturn.UseVisualStyleBackColor = true;
            btnReturn.Click += BtnReturn_Click;
            // 
            // gvGameID
            // 
            gvGameID.HeaderText = "GameID";
            gvGameID.MinimumWidth = 10;
            gvGameID.Name = "gvGameID";
            gvGameID.Width = 200;
            // 
            // gameIDGridView
            // 
            gameIDGridView.AllowUserToAddRows = false;
            gameIDGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gameIDGridView.Columns.AddRange(new DataGridViewColumn[] { gvGameID });
            gameIDGridView.GridColor = Color.CadetBlue;
            gameIDGridView.Location = new Point(951, 736);
            gameIDGridView.Name = "gameIDGridView";
            gameIDGridView.RowHeadersVisible = false;
            gameIDGridView.RowHeadersWidth = 82;
            gameIDGridView.RowTemplate.Height = 41;
            gameIDGridView.Size = new Size(293, 300);
            gameIDGridView.TabIndex = 8;
            gameIDGridView.CellDoubleClick += GameID_DoubleClick;
            // 
            // ChessMenu
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2431, 1289);
            Controls.Add(gameIDGridView);
            Controls.Add(btnReturn);
            Controls.Add(btnEnter);
            Controls.Add(TBServerName);
            Controls.Add(btnJoinGame);
            Controls.Add(btnNewGame);
            Controls.Add(labelTitle);
            Controls.Add(btnOnline);
            Controls.Add(btnPass);
            Margin = new Padding(4, 2, 4, 2);
            Name = "ChessMenu";
            Text = "ChessMenu";
            Load += ChessMenu_Load;
            ((System.ComponentModel.ISupportInitialize)gameIDGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnPass;
        private Button btnOnline;
        private Label labelTitle;
        private Button btnNewGame;
        private Button btnJoinGame;
        private TextBox TBServerName;
        private Button btnEnter;
        private System.Windows.Forms.Timer TimerConnect;
        private System.Windows.Forms.Timer TimerCheckEvents;
        private Button btnReturn;
        private DataGridViewTextBoxColumn gvGameID;
        private DataGridView gameIDGridView;
    }
}