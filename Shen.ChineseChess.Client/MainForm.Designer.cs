namespace Shen.ChineseChess.Client
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            this.btnReady = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.picTurn = new System.Windows.Forms.PictureBox();
            this.chessboard = new Shen.ChineseChess.Client.Chessboard();
            ((System.ComponentModel.ISupportInitialize)(this.picTurn)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReady
            // 
            this.btnReady.Location = new System.Drawing.Point(411, 12);
            this.btnReady.Name = "btnReady";
            this.btnReady.Size = new System.Drawing.Size(75, 23);
            this.btnReady.TabIndex = 0;
            this.btnReady.Text = "准备";
            this.btnReady.UseVisualStyleBackColor = true;
            this.btnReady.Click += new System.EventHandler(this.btnReady_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            // 
            // picTurn
            // 
            this.picTurn.BackColor = System.Drawing.Color.Transparent;
            this.picTurn.Location = new System.Drawing.Point(411, 66);
            this.picTurn.Name = "picTurn";
            this.picTurn.Size = new System.Drawing.Size(75, 68);
            this.picTurn.TabIndex = 2;
            this.picTurn.TabStop = false;
            // 
            // chessboard
            // 
            this.chessboard.BackColor = System.Drawing.Color.Transparent;
            this.chessboard.Chess = null;
            this.chessboard.Color = Shen.ChineseChess.ChessmanColor.Red;
            this.chessboard.Location = new System.Drawing.Point(12, 7);
            this.chessboard.Name = "chessboard";
            this.chessboard.Size = new System.Drawing.Size(355, 414);
            this.chessboard.TabIndex = 3;
            this.chessboard.Text = "chessboard";
            this.chessboard.Moved += new System.EventHandler<Shen.ChineseChess.Client.ChessboardMovedEventArgs>(this.chessboard_Moved);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Shen.ChineseChess.Client.Resource.bg;
            this.ClientSize = new System.Drawing.Size(518, 433);
            this.Controls.Add(this.chessboard);
            this.Controls.Add(this.picTurn);
            this.Controls.Add(this.btnReady);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picTurn)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReady;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox picTurn;
        private Chessboard chessboard;
    }
}