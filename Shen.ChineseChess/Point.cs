using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shen.ChineseChess
{
    [Serializable]
    public struct Point
    {
        int _y;

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }
        int _x;

        public int X
        {
            get { return _x; }
            set { _x = value; }
        }


        public Point(int x,int y)
        {
            _x = x;
            _y = y;
        }

        public Point Offset(int x, int y) {
            return new Point(_x + x, _y + y);
        }

        public Point Offset(Point p)
        {
            return new Point(_x + p._x, _y +p._y);
        }

        public Point Scale(double x, double y)
        {
            return new Point((int)(_x * x), (int)(_y * y));
        }

        public override string ToString()
        {
            return String.Format("X={0},Y={1}",_x,_y);
        }

    }
}
