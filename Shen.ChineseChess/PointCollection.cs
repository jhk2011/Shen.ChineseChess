using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shen.ChineseChess
{
    [Serializable]
    public class PointCollection : Collection<Point>
    {
        public void Add(int x, int y)
        {
            Items.Add(new Point(x, y));
        }
    }
}
