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
            client.ChessService.RoomChanged += ChessService_RoomChanged;
            client.ChessService.Join(rid);
        }

        private void ChessService_RoomChanged() {

            var room = client.ChessService.GetRoom();

            foreach (var player in room.Players) {

                if (player.Color == ChessmanColor.Red) {
                    lblRed.Text = GetName(room, player);
                }
                if (player.Color == ChessmanColor.Black) {
                    lblBlackName.Text = GetName(room, player);
                }
            }
        }

        private string GetName(ChessRoom room, ChessPlayer player) {
            if (player.IsReady && !room.IsReady) {
                return player.Name + "(已准备)";
            } else {
                return player.Name;
            }
        }

        private void ChessService_ChessBoardChanged() {

            var board = client.ChessService.GetChessBoard();

            if (board != null) {

                chessboard.Chess = board;

                if (board.Who == ChessmanColor.Red) {
                    picRed.BackgroundImage = Resource.r_box;
                    picBlack.BackgroundImage = null;
                } else {
                    picBlack.BackgroundImage = Resource.b_box;
                    picRed.BackgroundImage = null;
                }
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
