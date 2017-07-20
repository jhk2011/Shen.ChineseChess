using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shen.ChineseChess
{
    /// <summary>
    /// 表示一个玩家
    /// </summary>
    [Serializable]
    public class ChessPlayer
    {
        private int? number;
        /// <summary>
        /// 所在房间编号
        /// </summary>
        public int? Number
        {
            get { return number; }
            set { number = value; }
        }
        private string name;

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private bool isReady;

        /// <summary>
        /// 是否已准备
        /// </summary>
        public bool IsReady
        {
            get { return isReady; }
            set { isReady = value; }
        }

        public string Id { get; set; }
    }
}
