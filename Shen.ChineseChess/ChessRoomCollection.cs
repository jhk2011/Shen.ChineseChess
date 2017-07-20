using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shen.ChineseChess
{
    /// <summary>
    /// 房间的集合
    /// </summary>
    [Serializable]
    public class ChessRoomCollection:Collection<ChessRoom>
    {
        public override string ToString()
        {
            return String.Format("房间数量{0},进入房间的玩家数量{1}",Items.Count,Items.Sum(x=>x.Players.Count));
        }
    }
}
