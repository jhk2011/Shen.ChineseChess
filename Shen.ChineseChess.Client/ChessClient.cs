using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dao.Net;

namespace Shen.ChineseChess.Client {
    public class ChessClient : SocketClient {
        public IChessService ChessService { get; set; }

        public ChessClient() {

            var h = new ServiceClientHandler();

            this.Handlers.Add(h);

            ChessService = h.GetServiceProxy<IChessService>("chess");
        }

    }
}
