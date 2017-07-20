using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace Shen.Net.Common
{
    /// <summary>
    /// 表示服务器
    /// </summary>
    public class TcpServer
    {
        private int _port;

        Socket _server;

        /// <summary>
        /// 此服务器关联的底层套接字
        /// </summary>
        public Socket Server
        {
            get { return _server; }
        }

        private TcpHelperCollection _helpers = new TcpHelperCollection();

        /// <summary>
        /// 此服务器关联的所有客户端
        /// </summary>
        public TcpHelperCollection Helpers
        {
            get { return _helpers; }
        }

        private TcpHelper _helper;

        /// <summary>
        /// 
        /// </summary>
        public TcpHelper Helper
        {
            get { return _helper; }
        }

        private ProcessorCollection _processors = new ProcessorCollection();

        /// <summary>
        /// 服务端消息处理程序集合
        /// </summary>
        public ProcessorCollection Processors
        {
            get { return _processors; }
            set { _processors = value; }
        }


        public TcpServer(int port)
        {
            _port = port;

            _server = new Socket(SocketType.Stream, ProtocolType.Tcp);
            _server.Bind(new IPEndPoint(IPAddress.Loopback, _port));
            _server.Listen(10);

            _helper = new TcpHelper(_server);

            _helper.Accepted += _helper_Accepted;

        }

        void _helper_Accepted(object sender, AcceptedEventArgs e)
        {
            Console.WriteLine("新链接 {0}",_helpers.Count);

            TcpHelper helper = e.Helper;

            helper.ReceiveMessageAsync();

            helper.Received += helper_Received;
            helper.Closed += helper_Closed;

            _helpers.Add(helper);
        }

        private void helper_Closed(object sender, EventArgs e)
        {

            Console.WriteLine("断开连接 {0}",_helpers.Count);
            _helpers.Remove(sender as TcpHelper);
            Processors.Close(sender as TcpHelper);
        }

        private void helper_Received(object sender, RecitvedEventArgs e)
        {
            var helper = sender as TcpHelper;

            Console.WriteLine("消息到达");

            Console.WriteLine(e.Message);

            Processors.Process(new ProcessContext { Message = e.Message, Helper = (TcpHelper)(sender),ClientHelpers = _helpers });
        }

        /// <summary>
        /// 开始接收连接
        /// </summary>
        public void Accept()
        {
            _helper.AcceptAsync();
        }

        /// <summary>
        /// 发送消息给指定客户端
        /// </summary>
        /// <param name="message"></param>
        /// <param name="index"></param>
        public void Send(Message message, int index)
        {

            if (index < 0 || index >= _helpers.Count) throw new ArgumentOutOfRangeException("index");
            _helpers[index].SendMessageAsync(message);
        }

        /// <summary>
        /// 发送消息给所有客户端
        /// </summary>
        /// <param name="message"></param>
        public void Send(Message message)
        {
            for (int i = 0; i < _helpers.Count; i++)
            {
                _helpers[i].SendMessageAsync(message);
            }
        }

        public void Close() {
            for (int i = 0; i < _helpers.Count; i++)
            {
                _helpers[i].Close();
            }
        }

    }
}
