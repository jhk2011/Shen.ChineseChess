using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shen.ChineseChess
{
    public class ChessContext
    {
        ChessBoard _board = new ChessBoard();
        ChessmanColor _turn = ChessmanColor.Red;

        public ChessmanColor Turn
        {
            get { return _turn; }
        }

        public ChessBoard Board
        {
            get { return _board; }
            set { _board = value; }
        }

        public Boolean Move(Point p1, Point p2)
        {
            Chessman item = _board.Grid[p1.X, p1.Y];
            if (item.Color == _turn)
            {
                Board.Move(p1, p2);
                _turn = _turn == ChessmanColor.Red ? ChessmanColor.Black : ChessmanColor.Red;
                return true;
            }
            return false;
        }

    }
}
