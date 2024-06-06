using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie3
{
    public class Copier: BaseDevice
    {
        protected Printer printer = new Printer();
        protected Scanner scanner = new Scanner();

        private int _counter = 0;

        public int PrintCounter {  get => printer.PrintCounter; }
        public int ScanCounter { get => scanner.ScanCounter; }
        public int Counter { get => _counter; }

        public void PowerOff()
        {
            if (state == IDevice.State.on)
            {
                printer.PowerOff();
                scanner.PowerOff();

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

                state = IDevice.State.on;
                Console.WriteLine("Device is on ...");
                _counter++;
            }
        }

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            document = null;
            scanner.Scan(out document, formatType);
        }

        public void Print(in IDocument document)
        {
            printer.Print(document);
        }

        public void ScanAndPrint()
        {
            IDocument document;
            scanner.Scan(out document, IDocument.FormatType.JPG);
            printer.Print(document);
        }
    }
}
