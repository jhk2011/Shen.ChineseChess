using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shen.ChineseChess
{
    /// <summary>
    /// 棋子
    /// </summary>
    [Serializable]
    public class Chessman
    {

        private ChessmanType _type;
        private static String[] _reds = new string[] { "車", "馬", "相", "仕", "帅", "炮", "兵" };
        private static String[] _blacks = new string[] { "車", "馬", "象", "士", "将", "炮", "卒" };

        public Chessman(ChessmanType type, ChessmanColor color)
        {
            _type = type;
            _color = color;
        }

        public ChessmanType Type
        {
            get { return _type; }
            set { _type = value; }
        }
        private ChessmanColor _color;

        public ChessmanColor Color
        {
            get { return _color; }
            set { _color = value; }
        }


        public override string ToString()
        {
            int index = (int)_type;
            return _color == ChessmanColor.Red ? _reds[index] : _blacks[index];
        }

    }

}
