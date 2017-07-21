namespace Shen.ChineseChess.Gui
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnUndo = new System.Windows.Forms.Button();
            this.btnRedo = new System.Windows.Forms.Button();
            this.chessboard = new Shen.ChineseChess.Gui.Chessboard();
            this.SuspendLayout();
            // 
            // btnUndo
            // 
            this.btnUndo.Location = new System.Drawing.Point(355, 12);
            this.btnUndo.Name = "btnUndo";
            this.btnUndo.Size = new System.Drawing.Size(75, 23);
            this.btnUndo.TabIndex = 1;
            this.btnUndo.Text = "撤销";
            this.btnUndo.UseVisualStyleBackColor = true;
            this.btnUndo.Click += new System.EventHandler(this.btnUndo_Click);
            // 
            // btnRedo
            // 
            this.btnRedo.Location = new System.Drawing.Point(355, 51);
            this.btnRedo.Name = "btnRedo";
            this.btnRedo.Size = new System.Drawing.Size(75, 23);
            this.btnRedo.TabIndex = 2;
            this.btnRedo.Text = "重复";
            this.btnRedo.UseVisualStyleBackColor = true;
            this.btnRedo.Click += new System.EventHandler(this.btnRedo_Click);
            // 
            // chessboard
            // 
            this.chessboard.BackColor = System.Drawing.Color.Transparent;
            this.chessboard.Chess = null;
            this.chessboard.Color = Shen.ChineseChess.ChessmanColor.Red;
            this.chessboard.Location = new System.Drawing.Point(13, 12);
            this.chessboard.Name = "chessboard";
            this.chessboard.Size = new System.Drawing.Size(336, 402);
            this.chessboard.TabIndex = 3;
            this.chessboard.Text = "chessboard";
            this.chessboard.Moved += this.chessboard1_Moved;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Shen.ChineseChess.Gui.Resource.bg;
            this.ClientSize = new System.Drawing.Size(448, 426);
            this.Controls.Add(this.chessboard);
            this.Controls.Add(this.btnRedo);
            this.Controls.Add(this.btnUndo);
            this.Name = "MainForm";
            this.Text = "中国象棋";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnUndo;
        private System.Windows.Forms.Button btnRedo;
        private Chessboard chessboard;
    }
}

