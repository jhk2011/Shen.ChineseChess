using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shen.Net.Common
{
    public class ProcessContext
    {
        public Message Message { get; set; }
        public TcpHelper Helper { get; set; }
        public TcpHelperCollection ClientHelpers { get; set; }
    }
}
