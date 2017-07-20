using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Shen.Net.Common
{

    public class RecitvedEventArgs : EventArgs
    {
        public Message Message { get; set; }
    }

    public class AcceptedEventArgs : EventArgs
    {
        public TcpHelper Helper { get; set; }
    }
   
    public class TcpHelper
    {
        private Socket _socket;

        /// <summary>
        /// 此对象关联的底层套接字
        /// </summary>
        public Socket Socket
        {
            get { return _socket; }
            set { _socket = value; }
        }

        public TcpHelper(Socket socket)
        {
            _socket = socket;
        }

        private object _tag;

        public object Tag
        {
            get { return _tag; }
            set { _tag = value; }
        }

        #region 事件


        /// <summary>
        /// 接收到消息时发生
        /// </summary>
        public event EventHandler<RecitvedEventArgs> Received;

        protected void OnReceived(Message message)
        {
            if (Received != null)
            {
                Received(this, new RecitvedEventArgs { Message = message });
            }
        }

        /// <summary>
        /// 消息发送完成时发生
        /// </summary>
        public event EventHandler Sended;

        protected void OnSended()
        {
            if (Sended != null)
            {
                Sended(this, EventArgs.Empty);
            }
        }

        public event EventHandler Closed;

        /// <summary>
        /// 连接关闭时
        /// </summary>
        protected void OnClosed()
        {
            if (Closed != null)
            {
                Closed(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 接收到用户连接请求时
        /// </summary>
        public event EventHandler<AcceptedEventArgs> Accepted;

        protected void OnAccepted(TcpHelper helper)
        {
            if (Accepted != null)
            {
                Accepted(this, new AcceptedEventArgs { Helper = helper });
            }
        }

        #endregion

        /// <summary>
        /// 开始异步接收其他连接，接收其他连接后会继续接收
        /// </summary>
        public void AcceptAsync()
        {
            _socket.BeginAccept(ar =>
            {

                Socket client = _socket.EndAccept(ar);

                TcpHelper helper = new TcpHelper(client);

                OnAccepted(helper);

                AcceptAsync();

            }, null);

        }

        public void Close() {
            _socket.Close();
        }

        /// <summary>
        /// 异步发送消息
        /// </summary>
        /// <param name="message">要发送的消息</param>
        public void SendMessageAsync(Message message)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();

            formatter.Serialize(ms, message);

            int length = (int)ms.Length;

            MemoryStream ms2 = new MemoryStream();

            ms2.Write(BitConverter.GetBytes(length), 0, 4);
            ms2.Write(ms.GetBuffer(), 0, (int)ms.Length);

            //Console.WriteLine("Send Length {0}", length);

            //Console.WriteLine("Send Body {0}", message.Object);

            _socket.BeginSend(ms2.GetBuffer(), 0, (int)ms2.Length, SocketFlags.None, ar =>
            {

                _socket.EndSend(ar);

                //Console.WriteLine("Send Over");

                OnSended();

            }, null);

        }


        /// <summary>
        /// 异步接收消息，一个消息接受完成后会继续接收
        /// </summary>
        public void ReceiveMessageAsync()
        {

            byte[] buffer = new byte[4];

            //Console.WriteLine("Receive Length");

            _socket.BeginReceive(buffer, 0, 4, SocketFlags.None, ar =>
            {
                try
                {
                    _socket.EndReceive(ar);

                    int length = BitConverter.ToInt32(buffer, 0);

                    //Console.WriteLine("Receive Length {0}", length);

                    buffer = new byte[length];

                    _socket.BeginReceive(buffer, 0, length, SocketFlags.None, ar2 =>
                    {
                        try
                        {
                            _socket.EndReceive(ar2);

                            BinaryFormatter formatter = new BinaryFormatter();

                            MemoryStream ms = new MemoryStream(buffer, true);

                            Message message = new Message();


                            message = (Message)formatter.Deserialize(ms);

                            //Console.WriteLine("Receive Body {0}", message.Object);

                            OnReceived(message);

                            ReceiveMessageAsync();
                        }
                        catch (SocketException)
                        {
                            OnClosed();
                        }
                        


                    }, null);
                }
                catch (SocketException)
                {
                    OnClosed();
                }
                catch (ObjectDisposedException)
                {
                    OnClosed();
                }
            }, null);

        }

    }
}
