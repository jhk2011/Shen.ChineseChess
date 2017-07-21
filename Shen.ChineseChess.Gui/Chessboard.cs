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

    public class Chessboard : Control {


        bool selected = false;

        PointCollection points;

        Chess chess;
        public Chess Chess
        {
            get
            {
                return chess;
            }
            set
            {
                chess = value;
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

        public event EventHandler<ChessboardSelectedEventArgs> Selected;

        public event EventHandler<ChessboardMovedEventArgs> Moved;

        protected virtual void OnMoved(ChessboardMovedEventArgs e) {
            Moved?.Invoke(this, e);
        }

        protected virtual void OnSelected(ChessboardSelectedEventArgs e) {
            Selected?.Invoke(this, e);
        }

        protected override void OnMouseDown(MouseEventArgs e) {

            if (e.Button != MouseButtons.Left) return;

            if (chess == null || chess.Who != Color) return;

            Point p = VisualPointToChessPoint(new Point(e.X, e.Y), new Point(19, 19));

            if (point.Equals(p)) return;

            if (selected && points.Any(item => item.Equals(p))) {

                OnMoved(new ChessboardMovedEventArgs(point, p));

                selected = false;

                Invalidate();

            } else {

                if (chess.IsValid(p) && !chess.IsNull(p) && chess.HasColor(p, Color)) {

                    points = chess.GetMovePlaces(p);

                    selected = true;

                    point = p;

                    Invalidate();
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e) {
            DrawChessBoard(chess, e.Graphics);
        }


        #region 点的转换

        public static Point ChessPointToVisualPoint(Point p, Point offset) {
            Point p2 = new Point(p.X, Chess.Height - p.Y - 1);
            return p2.Scale(35, 36).Offset(24, 38).Offset(offset);
        }

        public static Point VisualPointToChessPoint(Point p, Point offset) {
            Point p2 = p.Offset(-24, -38)
                .Offset(offset)
                .Scale(1.0 / 35.0, 1.0 / 36.0);
            return new Point(p2.X, Chess.Height - p2.Y - 1);
        }

        #endregion

        #region 绘制

        Image image;

        public void DrawChessBoard(Chess board, Graphics g) {

            if (image != null) {
                bool flag = image== Resource.Board;
                Console.WriteLine("一致",flag);
            }

            image = Resource.Board;



            g.DrawImage(Resource.Board, 0, 0, 325, 402);

            if (board == null) return;

            if (board.Moved) {
                DrawBox(g, board.Source, board.Color);
                DrawBox(g, board.Target, board.Color);
            }

            board.ForEach((x, y, chessman) => {
                if (chessman == null) return;

                DrawChessman(g, new Point(x, y), chessman);
            });

            if (selected) {

                Chessman chessman = board.Grid[point.X, point.Y];

                DrawBox(g, point, chessman.Color);

                foreach (var item in points) {
                    DrawDot(g, item);
                }
            }
        }

        private void DrawChessman(Graphics g, Point point, Chessman item) {

            var image = GetChessImage(item);

            Point p = ChessPointToVisualPoint(point, new Point(-19, -19));

            g.DrawImage(image, p.X, p.Y, 38, 38);
        }

        private static void DrawDot(Graphics g, Point point) {
            Image image = Resource.dot;
            Point p = ChessPointToVisualPoint(new Point(point.X, point.Y), new Point(-9, -8));
            g.DrawImage(image, p.X, p.Y, 13, 11);
        }

        private void DrawBox(Graphics g, Point point, ChessmanColor color) {

            Image image = color == ChessmanColor.Red ? Resource.r_box : Resource.b_box;

            Point p = ChessPointToVisualPoint(point, new Point(-19, -19));

            g.DrawImage(image, p.X, p.Y, 38, 38);
        }

        private Image GetChessImage(Chessman item) {

            switch (item.Color) {
                case ChessmanColor.Red:
                    switch (item.Type) {
                        case ChessmanType.Ju:
                            return Resource.r_c;
                        case ChessmanType.Ma:
                            return Resource.r_m;
                        case ChessmanType.Xiang:
                            return Resource.r_x;
                        case ChessmanType.Shi:
                            return Resource.r_s;
                        case ChessmanType.Jiang:
                            return Resource.r_j;
                        case ChessmanType.Pao:
                            return Resource.r_p;
                        case ChessmanType.Bing:
                            return Resource.r_z;
                    }
                    break;
                case ChessmanColor.Black:
                    switch (item.Type) {
                        case ChessmanType.Ju:
                            return Resource.b_c;
                        case ChessmanType.Ma:
                            return Resource.b_m;
                        case ChessmanType.Xiang:
                            return Resource.b_x;
                        case ChessmanType.Shi:
                            return Resource.b_s;
                        case ChessmanType.Jiang:
                            return Resource.b_j;
                        case ChessmanType.Pao:
                            return Resource.b_p;
                        case ChessmanType.Bing:
                            return Resource.b_z;
                    }
                    break;
            }
            return null;
        }

        #endregion

    }
}
