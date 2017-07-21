using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shen.ChineseChess {
    /// <summary>
    /// 棋盘
    /// </summary>
    [Serializable]
    public partial class Chess {
        public const int Width = 9;
        public const int Height = 10;

        private Chessman[,] _grid = new Chessman[Width, Height];

        private ChessmanColor _who;

        public ChessmanColor Who
        {
            get { return _who; }
            set { _who = value; }
        }

        [NonSerialized]
        private ChessHistory _history = new ChessHistory();

        public Chess() {

        }

        public Chess(Chess board) {
            this._grid = (Chessman[,])board._grid.Clone();
            this._who = board._who;
        }

        [field: NonSerialized]
        public event Action ChessBoardChanged;

        private void OnChessBoardChanged() {
            ChessBoardChanged?.Invoke();
        }

        public Chessman[,] Grid
        {
            get { return _grid; }
            set { _grid = value; }
        }

        /// <summary>
        /// 初始化一局棋
        /// </summary>
        public void Initialize() {
            _grid[0, 0] = new Chessman(ChessmanType.Ju, ChessmanColor.Red);
            _grid[1, 0] = new Chessman(ChessmanType.Ma, ChessmanColor.Red);
            _grid[2, 0] = new Chessman(ChessmanType.Xiang, ChessmanColor.Red);
            _grid[3, 0] = new Chessman(ChessmanType.Shi, ChessmanColor.Red);

            _grid[4, 0] = new Chessman(ChessmanType.Jiang, ChessmanColor.Red);

            _grid[5, 0] = new Chessman(ChessmanType.Shi, ChessmanColor.Red);
            _grid[6, 0] = new Chessman(ChessmanType.Xiang, ChessmanColor.Red);
            _grid[7, 0] = new Chessman(ChessmanType.Ma, ChessmanColor.Red);
            _grid[8, 0] = new Chessman(ChessmanType.Ju, ChessmanColor.Red);

            _grid[1, 2] = new Chessman(ChessmanType.Pao, ChessmanColor.Red);
            _grid[7, 2] = new Chessman(ChessmanType.Pao, ChessmanColor.Red);

            _grid[0, 3] = new Chessman(ChessmanType.Bing, ChessmanColor.Red);
            _grid[2, 3] = new Chessman(ChessmanType.Bing, ChessmanColor.Red);
            _grid[4, 3] = new Chessman(ChessmanType.Bing, ChessmanColor.Red);
            _grid[6, 3] = new Chessman(ChessmanType.Bing, ChessmanColor.Red);
            _grid[8, 3] = new Chessman(ChessmanType.Bing, ChessmanColor.Red);


            _grid[0, 9] = new Chessman(ChessmanType.Ju, ChessmanColor.Black);
            _grid[1, 9] = new Chessman(ChessmanType.Ma, ChessmanColor.Black);
            _grid[2, 9] = new Chessman(ChessmanType.Xiang, ChessmanColor.Black);
            _grid[3, 9] = new Chessman(ChessmanType.Shi, ChessmanColor.Black);

            _grid[4, 9] = new Chessman(ChessmanType.Jiang, ChessmanColor.Black);

            _grid[5, 9] = new Chessman(ChessmanType.Shi, ChessmanColor.Black);
            _grid[6, 9] = new Chessman(ChessmanType.Xiang, ChessmanColor.Black);
            _grid[7, 9] = new Chessman(ChessmanType.Ma, ChessmanColor.Black);
            _grid[8, 9] = new Chessman(ChessmanType.Ju, ChessmanColor.Black);

            _grid[1, 7] = new Chessman(ChessmanType.Pao, ChessmanColor.Black);
            _grid[7, 7] = new Chessman(ChessmanType.Pao, ChessmanColor.Black);

            _grid[0, 6] = new Chessman(ChessmanType.Bing, ChessmanColor.Black);
            _grid[2, 6] = new Chessman(ChessmanType.Bing, ChessmanColor.Black);
            _grid[4, 6] = new Chessman(ChessmanType.Bing, ChessmanColor.Black);
            _grid[6, 6] = new Chessman(ChessmanType.Bing, ChessmanColor.Black);
            _grid[8, 6] = new Chessman(ChessmanType.Bing, ChessmanColor.Black);

            OnChessBoardChanged();
        }

        public void Update() {
            OnChessBoardChanged();
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            for (int j = 0; j < Height; j++) {
                sb.Append("|");
                for (int i = 0; i < Width; i++) {
                    if (_grid[i, j] == null) {
                        sb.Append("  |");
                    } else {
                        sb.Append(_grid[i, j].ToString() + "|");
                    }
                }
                sb.AppendLine();
                sb.AppendLine("----------------------------");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 对每个棋子进行操作
        /// </summary>
        /// <param name="f"></param>
        public void ForEach(Action<int, int, Chessman> f) {
            for (int j = 0; j < Height; j++) {
                for (int i = 0; i < Width; i++) {
                    f(i, j, _grid[i, j]);
                }
            }
        }

        /// <summary>
        /// 当前位置是否合法
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool IsValid(Point p) {
            return p.Y >= 0 && p.Y < Height && p.X >= 0 && p.X < Width;
        }

        /// <summary>
        /// 当前位置是否有棋子
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool IsNull(Point p) {
            if (!IsValid(p)) return false;
            return _grid[p.X, p.Y] == null;
        }

        public bool HasColor(Point p, ChessmanColor color) {
            if (!IsValid(p)) return false;
            Chessman item2 = _grid[p.X, p.Y];
            return item2 != null && item2.Color == color;
        }

        public bool CanEat(Point p, Chessman item) {
            if (!IsValid(p)) return false;
            Chessman item2 = _grid[p.X, p.Y];
            return item2 != null && item2.Color != item.Color;
        }


        public bool CanPlace(Point p, Chessman item) {
            if (!IsValid(p)) return false;
            return IsNull(p) || CanEat(p, item);
        }

        /// <summary>
        /// 是否在九宫格内
        /// </summary>
        /// <param name="p"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool InGrid(Point p, Chessman item) {

            if (p.X < 3 || p.X > 5) return false;

            if (item.Color == ChessmanColor.Red) {
                return p.Y >= 0 && p.Y <= 2;
            } else {
                return p.Y >= 7 && p.Y <= 9;
            }

        }

        /// <summary>
        /// 是否在河界内
        /// </summary>
        /// <param name="p"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool InRiver(Point p, Chessman item) {
            if (!IsValid(p)) return false;

            if (item.Color == ChessmanColor.Red) {
                return p.Y < 5;
            } else {
                return p.Y > 4;
            }
        }

        /// <summary>
        /// 获取一颗棋子的所有可行位置
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public PointCollection GetMovePlaces(Point p) {
            Chessman item = _grid[p.X, p.Y];

            if (item == null) throw new InvalidOperationException("没有棋子");

            PointCollection points = new PointCollection();

            if (item.Type == ChessmanType.Ju) {
                GetMovePlacesOfJu(p, item, points);
            } else if (item.Type == ChessmanType.Ma) {
                GetMovePlacesOfMa(p, item, points);
            } else if (item.Type == ChessmanType.Pao) {
                GetMovePlacesOfPao(p, item, points);
            } else if (item.Type == ChessmanType.Shi) {
                GetMovePlacesOfShi(p, item, points);
            } else if (item.Type == ChessmanType.Xiang) {
                GetMovePlacesOfXiang(p, item, points);
            } else if (item.Type == ChessmanType.Jiang) {
                GetMovePlacesOfJiang(p, item, points);
            } else if (item.Type == ChessmanType.Bing) {
                GetMovePlacesOfBing(p, item, points);
            }

            return points;
        }

        private void GetMovePlacesOfBing(Point p, Chessman item, PointCollection points) {
            PointCollection bings = new PointCollection();

            Point p2 = item.Color == ChessmanColor.Red ? p.Offset(0, 1) : p.Offset(0, -1);

            bings.Add(p2);

            if (!InRiver(p, item)) {
                bings.Add(p.Offset(-1, 0));
                bings.Add(p.Offset(1, 0));
            }

            foreach (var bing in bings) {
                if (CanPlace(bing, item)) points.Add(bing);
            }
        }

        private void GetMovePlacesOfJiang(Point p, Chessman item, PointCollection points) {
            PointCollection jiangs = new PointCollection() {
                    new Point(0,1),
                    new Point(0,-1),
                    new Point(1,0),
                    new Point(-1,0)
                };

            foreach (var j in jiangs) {
                Point p2 = p.Offset(j.X, j.Y);
                if (InGrid(p2, item) && CanPlace(p2, item)) points.Add(p2);

            }

            int x = p.X;

            int yStart, yStop, step;

            if (item.Color == ChessmanColor.Red) {
                yStart = p.Y + 1;
                yStop = Height - 1;
                step = 1;

                for (int y = yStart; y <= yStop; y += step) {

                    var c = _grid[x, y];

                    if (c != null) {
                        if (c.Type == ChessmanType.Jiang) {
                            points.Add(new Point { X = x, Y = y });
                        }
                        break;
                    }
                }

            } else {
                yStart = p.Y - 1;
                yStop = 0;
                step = -1;

                for (int y = yStart; y >= yStop; y += step) {

                    var c = _grid[x, y];

                    if (c != null) {
                        if (c.Type == ChessmanType.Jiang) {
                            points.Add(new Point { X = x, Y = y });
                        }
                        break;
                    }
                }
            }
        }

        private void GetMovePlacesOfXiang(Point p, Chessman item, PointCollection points) {
            PointCollection xiangs = new PointCollection()
                            {
                    new Point(2,2),
                    new Point(2,-2),
                    new Point(-2,2),
                    new Point(-2,-2),
                };

            PointCollection xiangyans = new PointCollection()
            {
                    new Point(1,1),
                    new Point(1,-1),
                    new Point(-1,1),
                    new Point(-1,-1),
                };

            for (int i = 0; i < xiangs.Count; i++) {
                Point xiang = xiangs[i];
                Point xiangyan = xiangyans[i];

                Point p2 = p.Offset(xiang.X, xiang.Y);
                Point p3 = p.Offset(xiangyan.X, xiangyan.Y);

                if (InRiver(p2, item) && CanPlace(p2, item) && IsNull(p3)) {
                    points.Add(p2);
                }

            }
        }

        private void GetMovePlacesOfShi(Point p, Chessman item, PointCollection points) {
            PointCollection shis = new PointCollection() {
                    new Point(1,1),
                    new Point(1,-1),
                    new Point(-1,1),
                    new Point(-1,-1)
                };

            foreach (var s in shis) {
                Point p2 = p.Offset(s.X, s.Y);
                if (InGrid(p2, item) && CanPlace(p2, item)) points.Add(p2);
            }
        }

        private void GetMovePlacesOfPao(Point p, Chessman item, PointCollection points) {
            PointCollection paos = new PointCollection();

            for (int i = 0; i < Width; i++) {
                Point p2 = new Point(i, p.Y);

                paos.Add(p2);
            }

            for (int j = 0; j < Height; j++) {
                Point p2 = new Point(p.X, j);
                paos.Add(p2);
            }

            foreach (var pao in paos) {
                if (!p.Equals(pao)) {
                    if ((IsNull(pao) && CalculateCount(p, pao) == 0) || (CanEat(pao, item) && CalculateCount(p, pao) == 1)) {
                        points.Add(pao);
                    }
                }
            }
        }

        private void GetMovePlacesOfMa(Point p, Chessman item, PointCollection points) {
            //日字形的点
            PointCollection ris = new PointCollection {
                    new Point(1,2),
                    new Point(-1,2),
                    new Point(1,-2),
                    new Point(-1,-2),
                    new Point(2,1),
                    new Point(-2,1),
                    new Point(2,-1),
                    new Point(-2,-1),
                };

            //对应的马腿
            PointCollection kos = new PointCollection()
            {
                    new Point(0,1),
                    new Point(0,1),
                    new Point(0,-1),
                    new Point(0,-1),
                    new Point(1,0),
                    new Point(-1,0),
                    new Point(1,0),
                    new Point(-1,0),
                };

            for (int i = 0; i < ris.Count; i++) {
                var ri = ris[i];
                var ko = kos[i];

                Point p2 = p.Offset(ri);
                Point p3 = p.Offset(ko);

                if (IsValid(p2) && CanPlace(p2, item) && IsNull(p3)) points.Add(p2);

            }
        }

        private void GetMovePlacesOfJu(Point p, Chessman item, PointCollection points) {
            PointCollection jus = new PointCollection();

            for (int y = 0; y < Height; y++) {
                Point p2 = new Point(p.X, y);
                jus.Add(p2);
            }

            for (int x = 0; x < Width; x++) {
                Point p2 = new Point(x, p.Y);
                jus.Add(p2);
            }

            foreach (var ju in jus) {
                if (!ju.Equals(p)) {
                    if (CanPlace(ju, item) && CalculateCount(p, ju) == 0) points.Add(ju);
                }
            }
        }

        public Chessman GetChess(Point p) {
            if (!IsValid(p)) throw new ArgumentOutOfRangeException("p");
            return _grid[p.X, p.Y];
        }

        public bool Moved { get; set; }

        public ChessmanColor Color { get; set; }
        public Point Source { get; set; }

        public Point Target { get; set; }

        /// <summary>
        /// 移动一颗棋子
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public void Move(Point source, Point target) {

            var item = GetChess(source);

            if (item == null) throw new InvalidOperationException("原位置没有棋子可以移动");

            if (item.Color != _who) throw new InvalidOperationException("没有轮到此方行走");

            PointCollection points = GetMovePlaces(source);

            if (!points.Any(x => x.Equals(target))) {
                throw new InvalidOperationException("目标位置非法");
            }

            Moved = true;
            Color = item.Color;
            Source = source;
            Target = target;

            _history.Add(this);

            _grid[target.X, target.Y] = item;

            _grid[source.X, source.Y] = null;

            OnChessBoardChanged();

            Turn();

        }

        /// <summary>
        /// 轮另一方走
        /// </summary>
        public void Turn() {
            _who = _who == ChessmanColor.Red ? ChessmanColor.Black : ChessmanColor.Red;
        }

        private void Assign(Chess board) {
            if (board == null) return;
            this._grid = board._grid;
            this._who = board._who;
        }

        public void Undo() {
            Assign(_history.Undo());
            OnChessBoardChanged();
        }

        public void Redo() {
            Assign(_history.Redo());
            OnChessBoardChanged();
        }

        public bool Checkmate(ChessmanColor color) {

            //TODO 检测将死

            return false;
        }

        /// <summary>
        /// 计算两点之间个隔子数量
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public int CalculateCount(Point p1, Point p2) {
            int n = 0;

            if (p1.X != p2.X && p1.Y != p2.Y) throw new InvalidOperationException("必须在同一行或同一列");

            if (p1.Y == p2.Y) {
                if (p1.X > p2.X) {
                    int t = p1.X;
                    p1.X = p2.X;
                    p2.X = t;
                }

                for (int x = p1.X + 1; x < p2.X; x++) {
                    if (!IsNull(new Point(x, p1.Y))) n++;
                }

            } else {
                if (p1.Y > p2.Y) {
                    int t = p1.Y;
                    p1.Y = p2.Y;
                    p2.Y = t;
                }

                for (int y = p1.Y + 1; y < p2.Y; y++) {
                    if (!IsNull(new Point(p1.X, y))) n++;
                }

            }
            return n;
        }

    }
}
