using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shen.ChineseChess
{
    public abstract class MovePointsProvider
    {
        ChessBoard _board;
        Point _point;

        public Point Point
        {
            get { return _point; }
            set { _point = value; }
        }

        public ChessBoard Board
        {
            get { return _board; }
            set { _board = value; }
        }

        public MovePointsProvider(ChessBoard board, Point point)
        {
            _board = board;
            _point = point;
        }

        public abstract PointCollection GetAllPoints();
        public abstract PointCollection GetPoints();
        public abstract bool CheckPoint(Point p);
        public abstract bool CheckPoint(Point p1,Point p2);

        public PointCollection GetMovePoint() {
            PointCollection ps1 = GetAllPoints();
            PointCollection ps2 = GetPoints();
            PointCollection ps3 = new PointCollection();
            if (ps2 == null)
            {
                for (int i = 0; i < ps1.Count; i++)
                {
                    Point p1 = ps1[i];
                    if (CheckPoint(p1)) ps3.Add(p1);
                }
            }
            else {
                for (int i = 0; i < ps1.Count; i++)
                {
                    Point p1 = ps1[i];
                    Point p2 = ps2[i];
                    if (CheckPoint(p1,p2)) ps3.Add(p1);
                }
            }
            return ps3;
        }

        public static MovePointsProvider GetProvider(ChessBoard board,Point p,ItemType type) {
            switch (type)
            {
                case ItemType.Ju:return new JuMovePointProvider(board, p);
                case ItemType.Ma: return new MaMovePointProvider(board, p);
                case ItemType.Xiang:
                    break;
                case ItemType.Shi:
                    break;
                case ItemType.Jiang:
                    break;
                case ItemType.Pao:
                    break;
                case ItemType.Bing:
                    break;
                default:
                    break;
            }
            return null;
        }
    }
}
