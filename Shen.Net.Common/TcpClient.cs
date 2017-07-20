using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace Shen.Net.Common
{
    /// <summary>
    /// 表示客户端
    /// </summary>
    public class TcpClient
    {
        Socket _client;

        public Socket Client
        {
            get { return _client; }
            set { _client = value; }
        }

        TcpHelper _helper;

        public TcpHelper Helper
        {
            get { return _helper; }
            set { _helper = value; }
        }

        String _host;

        int _port;

        public TcpClient(string host, int port)
        {
            _host = host;
            _port = port;
            _client = new Socket(SocketType.Stream, ProtocolType.Tcp);
            _helper = new TcpHelper(_client);
        }

        /// <summary>
        /// 连接到服务器
        /// </summary>
        public void Connect() {
            _client.Connect(_host, _port);
        }

        /// <summary>
        /// 开始接收数据
        /// </summary>
        public void Receive() {
            _helper.ReceiveMessageAsync();
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="message"></param>
        public void Send(Message message) {
            _helper.SendMessageAsync(message);
        }

    }

}
