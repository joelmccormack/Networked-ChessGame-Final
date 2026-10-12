namespace ChessGame
{
    partial class ChessUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btn_flip = new Button();
            btnResign = new Button();
            btnMenu = new Button();
            labelResult = new Label();
            SuspendLayout();
            // 
            // btn_flip
            // 
            btn_flip.Location = new Point(1490, 1099);
            btn_flip.Name = "btn_flip";
            btn_flip.Size = new Size(224, 83);
            btn_flip.TabIndex = 0;
            btn_flip.Text = "Flip Board";
            btn_flip.UseVisualStyleBackColor = true;
            btn_flip.Click += Btn_flip_Click;
            // 
            // btnResign
            // 
            btnResign.Location = new Point(1498, 1270);
            btnResign.Name = "btnResign";
            btnResign.Size = new Size(216, 86);
            btnResign.TabIndex = 1;
            btnResign.Text = "Resign";
            btnResign.UseVisualStyleBackColor = true;
            btnResign.Click += BtnResign_Click;
            // 
            // btnMenu
            // 
            btnMenu.Location = new Point(1490, 919);
            btnMenu.Name = "btnMenu";
            btnMenu.Size = new Size(224, 95);
            btnMenu.TabIndex = 2;
            btnMenu.Text = "Back To Menu";
            btnMenu.UseVisualStyleBackColor = true;
            btnMenu.Click += BtnMenu_Click;
            // 
            // labelResult
            // 
            labelResult.AutoSize = true;
            labelResult.Font = new Font("Segoe UI", 19.875F, FontStyle.Regular, GraphicsUnit.Point);
            labelResult.Location = new Point(1559, 276);
            labelResult.Name = "labelResult";
            labelResult.Size = new Size(0, 71);
            labelResult.TabIndex = 3;
            // 
            // ChessUI
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1965, 1488);
            Controls.Add(labelResult);
            Controls.Add(btnMenu);
            Controls.Add(btnResign);
            Controls.Add(btn_flip);
            Name = "ChessUI";
            Text = "ChessUI";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btn_flip;
        private Button btnResign;
        private Button btnMenu;
        private Label labelResult;
    }
}