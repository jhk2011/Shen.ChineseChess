using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shen.ChineseChess
{
    public class ChessHistory
    {
        List<Chess> _undos = new List<Chess>();
        List<Chess> _redos = new List<Chess>();

        public void Add(Chess board)
        {
            _undos.Add(new Chess(board));
        }

        public Chess Undo()
        {
            if (_undos.Count > 0)
            {
                int index = _undos.Count - 1;
                Chess board = _undos[index];
                _undos.RemoveAt(index);
                _redos.Add(board);
                return new Chess(board); ;
            }
            return null;
        }

        public Chess Redo() {
            if (_redos.Count > 0) {
                int index = _redos.Count - 1;
                Chess board = _redos[index];
                _redos.RemoveAt(index);
                _undos.Add(board);
                return new Chess(board);
            }
            return null;
        }



    }
}
