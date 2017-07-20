using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shen.ChineseChess
{
    /// <summary>
    /// 玩家集合
    /// </summary>
    [Serializable]
    public class ChessPlayerCollection : Collection<ChessPlayer>
    {
        public override string ToString()
        {
            return String.Format("玩家数量{0},进入房间数量{1},准备数量{2}", Items.Count, Items.Count(x => x.Number != null), Items.Count(x => x.IsReady));
        }
    }
}
