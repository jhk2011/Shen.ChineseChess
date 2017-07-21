using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Shen.ChineseChess.Client {
    public partial class MainForm : Form {

        public int rid;

        public ChessClient client;

        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            client.ChessService.ChessBoardChanged += ChessService_ChessBoardChanged;
            client.ChessService.Join(rid);
        }

        private void ChessService_ChessBoardChanged() {

            var board = client.ChessService.GetChessBoard();

            if (board != null) {

                chessboard.Chess = board;

                picTurn.Image = board.Who
                    == ChessmanColor.Black ? Resource.b_j : Resource.r_j;
            }
        }

        private void btnReady_Click(object sender, EventArgs e) {
            chessboard.Color = client.ChessService.Ready();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
            client.ChessService.Leave();
        }

        private void chessboard_Moved(object sender, ChessboardMovedEventArgs e) {
            client.ChessService.Move(e.Source, e.Target);
            //chessboard1.Color = chessboard1.Board.Who;
        }
    }
}
