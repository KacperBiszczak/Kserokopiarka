using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadanie1
{
    public class Copier: BaseDevice, IPrinter, IScanner
    {
        private int _printCounter = 0;
        private int _scanCounter = 0;
        private int _counter = 0;
        public int PrintCounter { get => _printCounter; }
        public int ScanCounter { get => _scanCounter; }
        public int Counter {  get => _counter; }

        public void PowerOff()
        {
            if(state == IDevice.State.on) 
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

        public void Scan(out IDocument document, IDocument.FormatType formatType = IDocument.FormatType.JPG)
        {
            document = null;

            if (state == IDevice.State.on)
            {
                switch (formatType)
                {
                    case IDocument.FormatType.PDF:
                        document = new PDFDocument($"PDFScan{ScanCounter}.pdf");
                        Console.WriteLine($"{DateTime.UtcNow} Scan: {document.GetFileName()}");
                        break;

                    case IDocument.FormatType.JPG:
                        document = new ImageDocument($"ImageScan{ScanCounter}.jpg");
                        Console.WriteLine($"{DateTime.UtcNow} Scan: {document.GetFileName()}");
                        break;

                    case IDocument.FormatType.TXT:
                        document = new TextDocument($"TextScan{ScanCounter}.txt");
                        Console.WriteLine($"{DateTime.UtcNow} Scan: {document.GetFileName()}");
                        break;

                    default:
                        throw new ArgumentException("Invalid format type.");

                }

                _scanCounter++;
            }
        }
   
        public void ScanAndPrint()
        {
            IDocument document;
            Scan(out document, IDocument.FormatType.JPG);
            Print(document);
        }
    }
}
