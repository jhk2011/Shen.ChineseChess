using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shen.ChineseChess {

    public interface IChessService {
        void Login(string name);
        ChessRoomCollection GetRooms();

        void Join(int rid);
        void Leave();

        event Action RoomChanged;
        ChessRoom GetRoom();

        ChessmanColor Ready();
        Chess GetChessBoard();

        event Action ChessBoardChanged;
        void Move(Point point, Point dest);
    }
}
