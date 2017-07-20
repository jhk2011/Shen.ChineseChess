using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shen.ChineseChess
{
    public class MaMovePointProvider : MovePointsProvider
    {
        static PointCollection _ris = new PointCollection {
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
        static PointCollection _kos = new PointCollection()
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

        public MaMovePointProvider(ChessBoard board, Point point)
            : base(board, point)
        {

        }

        public override PointCollection GetAllPoints()
        {
            PointCollection points = new PointCollection();
            foreach (var ma in _ris)
            {
                points.Add(Point.Offset(ma));
            }
            return points;
        }

        public override PointCollection GetPoints()
        {
            PointCollection points = new PointCollection();
            foreach (var item in _kos)
            {
                points.Add(Point.Offset(item));
            }
            return points;
        }

        public override bool CheckPoint(Point p)
        {
            return false;
        }

        public override bool CheckPoint(Point p1, Point p2)
        {
            ChessItem item = Board.Grid[Point.X, Point.Y];
            return Board.CanPlace(p1, item) && Board.IsNull(p2);
        }
    }
}
