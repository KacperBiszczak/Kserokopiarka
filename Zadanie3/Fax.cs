using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3
{
    public class Fax: BaseDevice, IFax
    {
        private int _faxCounter = 0;
        private int _counter = 0;

        public int FaxCounter { get => _faxCounter; }
        public int Counter { get => _counter; }

        public void PowerOff()
        {
            if (state == IDevice.State.on)
            {
                state = IDevice.State.off;
                Console.WriteLine("... Device is off !");
            }
        }

        public void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                state = IDevice.State.on;
                Console.WriteLine("Device is on ...");
                _counter++;
            }
        }

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
