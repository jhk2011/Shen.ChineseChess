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
            this.btnReady = new System.Windows.Forms.Button();
            this.picBlack = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblBlackName = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblRed = new System.Windows.Forms.Label();
            this.picRed = new System.Windows.Forms.PictureBox();
            this.chessboard = new Shen.ChineseChess.Client.Chessboard();
            ((System.ComponentModel.ISupportInitialize)(this.picBlack)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRed)).BeginInit();
            this.SuspendLayout();
            // 
            // btnReady
            // 
            this.btnReady.Location = new System.Drawing.Point(267, 410);
            this.btnReady.Name = "btnReady";
            this.btnReady.Size = new System.Drawing.Size(75, 23);
            this.btnReady.TabIndex = 0;
            this.btnReady.Text = "准备";
            this.btnReady.UseVisualStyleBackColor = true;
            this.btnReady.Click += new System.EventHandler(this.btnReady_Click);
            // 
            // picBlack
            // 
            this.picBlack.BackColor = System.Drawing.Color.Transparent;
            this.picBlack.Image = global::Shen.ChineseChess.Client.Resource.b_j;
            this.picBlack.Location = new System.Drawing.Point(28, 15);
            this.picBlack.Name = "picBlack";
            this.picBlack.Size = new System.Drawing.Size(38, 38);
            this.picBlack.TabIndex = 2;
            this.picBlack.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblBlackName);
            this.panel1.Controls.Add(this.picBlack);
            this.panel1.Location = new System.Drawing.Point(13, 45);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(108, 100);
            this.panel1.TabIndex = 4;
            // 
            // lblBlackName
            // 
            this.lblBlackName.AutoSize = true;
            this.lblBlackName.Location = new System.Drawing.Point(28, 69);
            this.lblBlackName.Name = "lblBlackName";
            this.lblBlackName.Size = new System.Drawing.Size(11, 12);
            this.lblBlackName.TabIndex = 3;
            this.lblBlackName.Text = "-";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.lblRed);
            this.panel2.Controls.Add(this.picRed);
            this.panel2.Location = new System.Drawing.Point(13, 193);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(108, 100);
            this.panel2.TabIndex = 5;
            // 
            // lblRed
            // 
            this.lblRed.AutoSize = true;
            this.lblRed.Location = new System.Drawing.Point(28, 69);
            this.lblRed.Name = "lblRed";
            this.lblRed.Size = new System.Drawing.Size(11, 12);
            this.lblRed.TabIndex = 3;
            this.lblRed.Text = "-";
            // 
            // picRed
            // 
            this.picRed.BackColor = System.Drawing.Color.Transparent;
            this.picRed.Image = global::Shen.ChineseChess.Client.Resource.r_j;
            this.picRed.Location = new System.Drawing.Point(28, 15);
            this.picRed.Name = "picRed";
            this.picRed.Size = new System.Drawing.Size(38, 38);
            this.picRed.TabIndex = 2;
            this.picRed.TabStop = false;
            // 
            // chessboard
            // 
            this.chessboard.BackColor = System.Drawing.Color.Transparent;
            this.chessboard.Chess = null;
            this.chessboard.Color = Shen.ChineseChess.ChessmanColor.Red;
            this.chessboard.Location = new System.Drawing.Point(135, 7);
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
            this.ClientSize = new System.Drawing.Size(518, 445);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnReady);
            this.Controls.Add(this.chessboard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picBlack)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picRed)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnReady;
        private System.Windows.Forms.PictureBox picBlack;
        private Chessboard chessboard;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblBlackName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblRed;
        private System.Windows.Forms.PictureBox picRed;
    }
}