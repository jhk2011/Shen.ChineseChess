using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Shen.ChineseChess.Gui {
    public partial class MainForm : Form {

        Chess chess = new Chess();
        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            chess.Initialize();
            chessboard.Chess = chess;
        }

        private void btnUndo_Click(object sender, EventArgs e) {
            chess.Undo();
            chessboard.Invalidate();
        }

        private void btnRedo_Click(object sender, EventArgs e) {
            chess.Redo();
            chessboard.Invalidate();
        }

        private void chessboard1_Moved(object sender, ChessboardMovedEventArgs e) {
            chessboard.Chess.Move(e.Source, e.Target);
            chessboard.Invalidate();
            chessboard.Color = chess.Who;

            if (chess.Checkmate(chess.Who)) {
                Console.WriteLine("{0}方被将死", chess.Who);
            }
        }
    }
}
