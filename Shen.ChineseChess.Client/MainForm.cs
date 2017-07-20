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

        bool _showHint = false;

        PointCollection _points;
        public ChessBoard _board;
        public bool _turn = false;
        public ChessmanColor _color;

        public int rid;

        public ChessClient client;

        public ChessBoard Board
        {
            get { return _board; }
            set { _board = value; }
        }

        Point _point;

        public MainForm() {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            client.ChessService.ChessBoardChanged += ChessService_ChessBoardChanged;
            client.ChessService.Join(rid);
        }

        private void ChessService_ChessBoardChanged() {

            _board = client.ChessService.GetChessBoard();

            if (_board != null) {

                _turn = _board.Who == _color;

                pictureBox1.Image = _board.Who
                    == ChessmanColor.Black
                    ? Resource.b_j : Resource.r_j;

                picBoard.Invalidate();
            }
        }

        private void btnReady_Click(object sender, EventArgs e) {
            _color = client.ChessService.Ready();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e) {
            client.ChessService.Leave();
        }

        #region 点的转换

        public static Point LogicPointToVisualPoint(Point p, Point offset) {
            Point p2 = new Point(p.X, ChessBoard.Height - p.Y - 1);
            return p2.Scale(35, 36).Offset(24, 38).Offset(offset);
        }

        public static Point VisualPointToLogicPoint(Point p, Point offset) {
            Point p2 = p.Offset(-24, -38)
                .Offset(offset)
                .Scale(1.0 / 35.0, 1.0 / 36.0);
            return new Point(p2.X, ChessBoard.Height - p2.Y - 1);
        }

        #endregion

        #region 绘制
        public void DrawChessBoard(ChessBoard board, Graphics g) {

            g.DrawImage(Resource.Board, 0, 0, 325, 402);

            board.ForEach((x, y, item) => {

                if (item != null) {
                    Image image = null;

                    #region 获取图片
                    switch (item.Color) {
                        case ChessmanColor.Red:
                            switch (item.Type) {
                                case ChessmanType.Ju:
                                    image = Resource.r_c;
                                    break;
                                case ChessmanType.Ma:
                                    image = Resource.r_m;
                                    break;
                                case ChessmanType.Xiang:
                                    image = Resource.r_x;
                                    break;
                                case ChessmanType.Shi:
                                    image = Resource.r_s;
                                    break;
                                case ChessmanType.Jiang:
                                    image = Resource.r_j;
                                    break;
                                case ChessmanType.Pao:
                                    image = Resource.r_p;
                                    break;
                                case ChessmanType.Bing:
                                    image = Resource.r_z;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        case ChessmanColor.Black:
                            switch (item.Type) {
                                case ChessmanType.Ju:
                                    image = Resource.b_c;
                                    break;
                                case ChessmanType.Ma:
                                    image = Resource.b_m;
                                    break;
                                case ChessmanType.Xiang:
                                    image = Resource.b_x;
                                    break;
                                case ChessmanType.Shi:
                                    image = Resource.b_s;
                                    break;
                                case ChessmanType.Jiang:
                                    image = Resource.b_j;
                                    break;
                                case ChessmanType.Pao:
                                    image = Resource.b_p;
                                    break;
                                case ChessmanType.Bing:
                                    image = Resource.b_z;
                                    break;
                                default:
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                    #endregion

                    Point p = LogicPointToVisualPoint(new Point(x, y), new Point(-19, -19));

                    //MessageBox.Show(p.ToString());

                    g.DrawImage(image, p.X, p.Y, 38, 38);
                    //g.DrawRectangle(Pens.Black, p.X, p.Y, 35, 36);
                }
            });

            if (_showHint) {
                foreach (var item in _points) {
                    Image image2 = Resource.dot;
                    Point p2 = LogicPointToVisualPoint(new Point(item.X, item.Y), new Point(-9, -8));
                    g.DrawImage(image2, p2.X, p2.Y, 13, 11);
                }

                Point p = LogicPointToVisualPoint(_point, new Point(-19, -19));

                Chessman cItem = board.Grid[_point.X, _point.Y];

                Image image = Resource.b_box;

                if (cItem.Color == ChessmanColor.Red) {
                    image = Resource.r_box;
                }

                g.DrawImage(image, p.X, p.Y, 38, 38);

            }
        }

        #endregion

        private void imgBoard_MouseDown(object sender, MouseEventArgs e) {
            if (!_turn) return;

            if (e.Button == MouseButtons.Left) {

                Point p = VisualPointToLogicPoint(new Point(e.X, e.Y), new Point(19, 19));

                if (!_showHint) {
                    if (_board.IsValid(p) && !_board.IsNull(p) && _board.HasColor(p, _color)) {
                        _points = _board.GetMovePlaces(p);
                        _point = p;
                        _showHint = _points.Count > 0;
                        picBoard.Invalidate();
                    }
                } else {
                    if (_point.Equals(p)) {
                        //_showHint = false;
                        //picBoard.Invalidate();
                        return;
                    }

                    if (_points.Any(item => item.Equals(p))) {

                        // _board.Move(_point, p);

                        client.ChessService.Move(_point, p);

                        _showHint = false;
                        picBoard.Invalidate();
                        return;
                    }

                    if (_board.IsValid(p) && !_board.IsNull(p) && _board.HasColor(p, _color)) {
                        _points = _board.GetMovePlaces(p);
                        _point = p;
                        _showHint = _points.Count > 0;
                        picBoard.Invalidate();
                    } else {
                        //_showHint = false;
                        //picBoard.Invalidate();
                    }

                }

            }
        }

        private void picBoard_Paint(object sender, PaintEventArgs e) {
            if (_board != null) {
                DrawChessBoard(_board, e.Graphics);
            }
        }

        private void timer1_Tick(object sender, EventArgs e) {

        }

        private void picBoard_Click(object sender, EventArgs e) {

        }
    }
}
