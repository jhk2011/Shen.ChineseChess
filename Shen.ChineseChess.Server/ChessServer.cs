using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Dao.Net;

namespace Shen.ChineseChess.Server {

    class ChessServer : SocketServer {

        RoomService roomService = new RoomService();

        internal RoomService RoomService
        {
            get
            {
                return roomService;
            }
        }

        protected override SocketSession GetSession(Socket client) {
            return new ChessSession(client, roomService);
        }
    }

    class ChessSession : SocketSession {

        public ChessSession(Socket socket, RoomService rs) : base(socket) {

            var h = new ServiceHandler();

            this.Handlers.Add(h);

            h.AddService("chess", new ChessService(rs));
        }
        public string Name { get; internal set; }
        public ChessPlayer Player { get; internal set; }

        public ChessService ChessService { get; internal set; }
    }
}
