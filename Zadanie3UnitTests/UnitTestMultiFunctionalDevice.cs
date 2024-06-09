using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace Zadanie3
{

    [TestClass]
    public class UnitTestMultiFunctionalDevice
    {
        [TestMethod]
        public void MultiFunctionalDevice_GetState_StateOff()
        {
            var multiFunctionalDevice = new MultiFunctionalDevice();
            multiFunctionalDevice.PowerOff();

            Assert.AreEqual(IDevice.State.off, multiFunctionalDevice.GetState());
        }

        [TestMethod]
        public void MultiFunctionalDevice_GetState_StateOn()
        {
            var multiFunctionalDevice = new MultiFunctionalDevice();
            multiFunctionalDevice.PowerOn();

            Assert.AreEqual(IDevice.State.on, multiFunctionalDevice.GetState());
        }

        [TestMethod]
        public void MultiFunctionalDevice_Print_DeviceOn()
        {
            var multiFunctionalDevice = new MultiFunctionalDevice();
            multiFunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multiFunctionalDevice.Print(in doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultiFunctionalDevice_Print_DeviceOff()
        {
            var multiFunctionalDevice = new MultiFunctionalDevice();
            multiFunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1 = new PDFDocument("aaa.pdf");
                multiFunctionalDevice.Print(in doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultiFunctionalDevice_Scan_DeviceOff()
        {
            var multiFunctionalDevice = new MultiFunctionalDevice();
            multiFunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multiFunctionalDevice.Scan(out doc1);
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultiFunctionalDevice_Scan_DeviceOn()
        {
            var multiFunctionalDevice = new MultiFunctionalDevice();
            multiFunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multiFunctionalDevice.Scan(out doc1);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultiFunctionalDevice_Scan_FormatTypeDocument()
        {
            var multiFunctionalDevice = new MultiFunctionalDevice();
            multiFunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                IDocument doc1;
                multiFunctionalDevice.Scan(out doc1, formatType: IDocument.FormatType.JPG);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".jpg"));

                multiFunctionalDevice.Scan(out doc1, formatType: IDocument.FormatType.TXT);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".txt"));

                multiFunctionalDevice.Scan(out doc1, formatType: IDocument.FormatType.PDF);
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains(".pdf"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultiFunctionalDevice_ScanAndPrint_DeviceOn()
        {
            var multiFunctionalDevice = new MultiFunctionalDevice();
            multiFunctionalDevice.PowerOn();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multiFunctionalDevice.ScanAndPrint();
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsTrue(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultiFunctionalDevice_ScanAndPrint_DeviceOff()
        {
            var multiFunctionalDevice = new MultiFunctionalDevice();
            multiFunctionalDevice.PowerOff();

            var currentConsoleOut = Console.Out;
            currentConsoleOut.Flush();
            using (var consoleOutput = new ConsoleRedirectionToStringWriter())
            {
                multiFunctionalDevice.ScanAndPrint();
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Scan"));
                Assert.IsFalse(consoleOutput.GetOutput().Contains("Print"));
            }
            Assert.AreEqual(currentConsoleOut, Console.Out);
        }

        [TestMethod]
        public void MultiFunctionalDevice_PrintCounter()
        {
            var multiFunctionalDevice = new MultiFunctionalDevice();
            multiFunctionalDevice.PowerOn();

            IDocument doc1 = new PDFDocument("aaa.pdf");
            multiFunctionalDevice.Print(in doc1);
            IDocument doc2 = new TextDocument("aaa.txt");
            multiFunctionalDevice.Print(in doc2);
            IDocument doc3 = new ImageDocument("aaa.jpg");
            multiFunctionalDevice.Print(in doc3);

            multiFunctionalDevice.PowerOff();
            multiFunctionalDevice.Print(in doc3);
            multiFunctionalDevice.Scan(out doc1);
            multiFunctionalDevice.PowerOn();

            multiFunctionalDevice.ScanAndPrint();
            multiFunctionalDevice.ScanAndPrint();

            Assert.AreEqual(5, multiFunctionalDevice.PrintCounter);
        }

        [TestMethod]
        public void MultiFunctionalDevice_ScanCounter()
        {
            var multiFunctionalDevice = new MultiFunctionalDevice();
            multiFunctionalDevice.PowerOn();

            IDocument doc1;
            multiFunctionalDevice.Scan(out doc1);
            IDocument doc2;
            multiFunctionalDevice.Scan(out doc2);

            IDocument doc3 = new ImageDocument("aaa.jpg");
            multiFunctionalDevice.Print(in doc3);

            multiFunctionalDevice.PowerOff();
            multiFunctionalDevice.Print(in doc3);
            multiFunctionalDevice.Scan(out doc1);
            multiFunctionalDevice.PowerOn();

            multiFunctionalDevice.ScanAndPrint();
            multiFunctionalDevice.ScanAndPrint();

            Assert.AreEqual(4, multiFunctionalDevice.ScanCounter);
        }

        [TestMethod]
        public void MultiFunctionalDevice_PowerOnCounter()
        {
            var multiFunctionalDevice = new MultiFunctionalDevice();
            multiFunctionalDevice.PowerOn();
            multiFunctionalDevice.PowerOn();
            multiFunctionalDevice.PowerOn();

            IDocument doc1;
            multiFunctionalDevice.Scan(out doc1);
            IDocument doc2;
            multiFunctionalDevice.Scan(out doc2);

            multiFunctionalDevice.PowerOff();
            multiFunctionalDevice.PowerOff();
            multiFunctionalDevice.PowerOff();
            multiFunctionalDevice.PowerOn();

            IDocument doc3 = new ImageDocument("aaa.jpg");
            multiFunctionalDevice.Print(in doc3);

            multiFunctionalDevice.PowerOff();
            multiFunctionalDevice.Print(in doc3);
            multiFunctionalDevice.Scan(out doc1);
            multiFunctionalDevice.PowerOn();

            multiFunctionalDevice.ScanAndPrint();
            multiFunctionalDevice.ScanAndPrint();

            Assert.AreEqual(3, multiFunctionalDevice.Counter);
        }

    }
}
