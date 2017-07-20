using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shen.ChineseChess
{
    public class JuMovePointProvider:MovePointsProvider
    {
        public JuMovePointProvider(ChessBoard board, Point point)
            : base(board, point)
        {

        }

        public override bool CheckPoint(Point p)
        {
            ChessItem item = Board.Grid[Point.X, Point.Y];
            return Board.CanPlace(p,item) && Board.Gezi(Point,p)==0;
        }

        public override bool CheckPoint(Point p1, Point p2)
        {
            return false;
        }

        public override PointCollection GetAllPoints()
        {
            PointCollection points = new PointCollection();

            for (int y = 0; y < ChessBoard.ChessBoardHeight; y++)
            {
                Point p2 = new Point(Point.X, y);
                points.Add(p2);
            }

            for (int x = 0; x < ChessBoard.ChessBoardWidth; x++)
            {
                Point p2 = new Point(x, Point.Y);
                points.Add(p2);
            }

            return points;
        }

        public override PointCollection GetPoints()
        {
            return null;
        }
    }
}
