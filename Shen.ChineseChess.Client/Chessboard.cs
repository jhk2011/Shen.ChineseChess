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

    public class ChessboardMovedEventArgs : EventArgs {

        public ChessboardMovedEventArgs(Point source, Point target) {
            Source = source;
            Target = target;
        }

        public Point Source { get; private set; }

        public Point Target { get; private set; }
    }

    public class ChessboardSelectedEventArgs : EventArgs {
        public ChessboardSelectedEventArgs(Point point) {
            Point = point;
        }

        public Point Point { get; private set; }
    }


    public delegate void ChessboardMovedEventHandler(object sender, ChessboardMovedEventArgs e);
    public class Chessboard : Control {


        bool selected = false;

        PointCollection points;

        ChessBoard board;
        public ChessBoard Board
        {
            get
            {
                return board;
            }
            set
            {
                board = value;
                Invalidate();
            }
        }


        ChessmanColor color;

        public ChessmanColor Color
        {
            get
            {
                return color;
            }

            set
            {
                color = value;
            }
        }

        Point point;

        public Point Point
        {
            get
            {
                return point;
            }
        }


        #region 点的转换

        public static Point ChessPointToVisualPoint(Point p, Point offset) {
            Point p2 = new Point(p.X, ChessBoard.Height - p.Y - 1);
            return p2.Scale(35, 36).Offset(24, 38).Offset(offset);
        }

        public static Point VisualPointToChessPoint(Point p, Point offset) {
            Point p2 = p.Offset(-24, -38)
                .Offset(offset)
                .Scale(1.0 / 35.0, 1.0 / 36.0);
            return new Point(p2.X, ChessBoard.Height - p2.Y - 1);
        }

        #endregion

        #region 绘制
        public void DrawChessBoard(ChessBoard board, Graphics g) {

            g.DrawImage(Resource.Board, 0, 0, 325, 402);

            if (board == null) return;

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

                    Point p = ChessPointToVisualPoint(new Point(x, y), new Point(-19, -19));

                    //MessageBox.Show(p.ToString());

                    g.DrawImage(image, p.X, p.Y, 38, 38);
                    //g.DrawRectangle(Pens.Black, p.X, p.Y, 35, 36);
                }
            });

            if (selected) {
                foreach (var item in points) {
                    Image image2 = Resource.dot;
                    Point p2 = ChessPointToVisualPoint(new Point(item.X, item.Y), new Point(-9, -8));
                    g.DrawImage(image2, p2.X, p2.Y, 13, 11);
                }

                Point p = ChessPointToVisualPoint(Point, new Point(-19, -19));

                Chessman chessman = board.Grid[Point.X, Point.Y];

                Image image = Resource.b_box;

                if (chessman.Color == ChessmanColor.Red) {
                    image = Resource.r_box;
                }

                g.DrawImage(image, p.X, p.Y, 38, 38);

            }
        }

        #endregion

        protected new virtual void Paint() {
            this.Invalidate();
        }

        public event EventHandler<ChessboardSelectedEventArgs> Selected;

        public event ChessboardMovedEventHandler Moved;

        protected virtual void OnMoved(ChessboardMovedEventArgs e) {
            Moved?.Invoke(this, e);
        }

        protected virtual void OnSelected(ChessboardSelectedEventArgs e) {
            Selected?.Invoke(this, e);
        }

        protected override void OnMouseDown(MouseEventArgs e) {

            if (e.Button != MouseButtons.Left) return;

            if (board == null || board.Who != Color) return;

            Point p = VisualPointToChessPoint(new Point(e.X, e.Y), new Point(19, 19));

            if (point.Equals(p)) {
                OnSelected(new ChessboardSelectedEventArgs(point));
                return;
            }

            if (selected && points.Any(item => item.Equals(p))) {

                board.Move(point, p);

                OnMoved(new ChessboardMovedEventArgs(point, p));

                selected = false;

                Paint();

            } else {

                if (board.IsValid(p) && !board.IsNull(p) && board.HasColor(p, Color)) {

                    points = board.GetMovePlaces(p);

                    selected = true;
                    point = p;

                    Paint();
                }
            }
        }

        public Chessboard() {

            foreach (var style in typeof(ControlStyles).GetEnumValues()) {
                Console.WriteLine("{0}={1}", typeof(ControlStyles)
                    .GetEnumName(style),
                    GetStyle((ControlStyles)style));
            }
           

            SetStyle(ControlStyles.SupportsTransparentBackColor
                  | ControlStyles.UserPaint
                  | ControlStyles.AllPaintingInWmPaint
                  | ControlStyles.OptimizedDoubleBuffer
                  , true);
        }

        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //       cp.ExStyle |= 0x00000020; //WS_EX_TRANSPARENT 
        //        return cp;
        //    }
        //}

        protected override void OnPaint(PaintEventArgs e) {
            DrawChessBoard(board, e.Graphics);
        }
    }
}
