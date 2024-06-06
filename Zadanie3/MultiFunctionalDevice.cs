using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3
{
    public class MultiFunctionalDevice : Copier
    {
        protected Fax fax = new Fax();
        
        public int _counter = 0;

        public int FaxCounter { get => fax.FaxCounter; }
        public int Counter { get => Counter; }

        public void PowerOff()
        {
            if (state == IDevice.State.on)
            {
                printer.PowerOff();
                scanner.PowerOff();
                fax.PowerOff();

                state = IDevice.State.off;
                Console.WriteLine("... Device is off !");
            }
        }

        public void PowerOn()
        {
            if (state == IDevice.State.off)
            {
                printer.PowerOn();
                scanner.PowerOn();
                fax.PowerOn();

                state = IDevice.State.on;
                Console.WriteLine("Device is on ...");
                _counter++;
            }
        }

        public void SendFax(in IDocument document, string recipient)
        {
            fax.SendFax(document, recipient);
        }


    }
}
