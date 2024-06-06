using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3
{
    public class Printer : BaseDevice, IPrinter
    {
        private int _printCounter = 0;
        private int _counter = 0;

        public int PrintCounter { get => _printCounter; }
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

        public void Print(in IDocument document)
        {
            if (state == IDevice.State.on)
            {
                Console.WriteLine($"{DateTime.UtcNow} Print: {document.GetFileName()}");
                _printCounter++;
            }
        }

    }
}
