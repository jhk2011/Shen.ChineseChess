using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace Shen.Net.Common
{
    public class ProcessorCollection:Collection<IProcessor>,IProcessor
    {
        
        #region IProcessor 成员

        public bool Process(ProcessContext context) {
            foreach (var item in this)
            {
                if (item.Process(context)) {
                    return true;
                }   
            }
            return false;
        }


        public bool Close(TcpHelper helper)
        {
            foreach (var item in this)
            {
                if (item.Close(helper))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
