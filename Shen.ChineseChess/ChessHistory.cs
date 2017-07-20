using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shen.ChineseChess
{
    public class ChessHistory
    {
        List<ChessBoard> _undos = new List<ChessBoard>();
        List<ChessBoard> _redos = new List<ChessBoard>();

        public void Add(ChessBoard board)
        {
            _undos.Add(new ChessBoard(board));
        }

        public ChessBoard Undo()
        {
            if (_undos.Count > 0)
            {
                int index = _undos.Count - 1;
                ChessBoard board = _undos[index];
                _undos.RemoveAt(index);
                _redos.Add(board);
                return new ChessBoard(board); ;
            }
            return null;
        }

        public ChessBoard Redo() {
            if (_redos.Count > 0) {
                int index = _redos.Count - 1;
                ChessBoard board = _redos[index];
                _redos.RemoveAt(index);
                _undos.Add(board);
                return new ChessBoard(board);
            }
            return null;
        }



    }
}
