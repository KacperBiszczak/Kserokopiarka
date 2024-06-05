using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ver1;

namespace ClassLibrary
{
    public interface IFax : IDevice
    {
        void SendFax(in IDocument document, string recipient);
    }
}
