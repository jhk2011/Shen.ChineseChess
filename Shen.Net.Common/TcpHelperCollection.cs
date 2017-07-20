using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections.ObjectModel;

namespace Shen.Net.Common
{
    public class TcpHelperCollection:Collection<TcpHelper>
    {
        public TcpHelperCollection()
        {

        }

        public TcpHelperCollection(IList<TcpHelper> helpers):base(helpers)
        {
            
        }

        public void SendMessageAsync(Message message)
        {
            foreach (var item in Items)
            {
                item.SendMessageAsync(message);
            }
        }
    }
}
