using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ver1;

namespace ClassLibrary
{
    public class MultiFunctionalDevice : Copier, IFax
    {
        private int _faxCounter = 0;

        public int FaxCounter { get => _faxCounter; }

        public void SendFax(in IDocument document, string recipient)
        {
            if (state == IDevice.State.on)
            {
                Console.WriteLine($"{DateTime.UtcNow} Fax: {document.GetFileName()} To: {recipient}");
                _faxCounter++;
            }
        }
    }
}
