using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Zadanie2
{
    [TestClass]
    public class UnitTestMultiFunctionalDevice
    {
        [TestMethod]
        public void MultiFunctionalDevice_TestPowerOn()
        {
            var device = new MultiFunctionalDevice();
            device.PowerOff();
            device.PowerOff();
            device.PowerOn();
            device.PowerOn();
            Assert.AreEqual(1, device.Counter);
            Assert.AreEqual(IDevice.State.on, device.GetState());
        }

        [TestMethod]
        public void MultiFunctionalDevice_TestPowerOff()
        {
            var device = new MultiFunctionalDevice();
            device.PowerOn();
            device.PowerOn();
            device.PowerOff();
            device.PowerOff();
            Assert.AreEqual(IDevice.State.off, device.GetState());
        }

        [TestMethod]
        public void MultiFunctionalDevice_TestPrint()
        {
            var device = new MultiFunctionalDevice();
            device.PowerOn();

            IDocument doc = new PDFDocument("test.pdf");
            device.Print(in doc);
            Assert.AreEqual(1, device.PrintCounter);
        }

        [TestMethod]
        public void MultiFunctionalDevice_TestScan()
        {
            var device = new MultiFunctionalDevice();
            device.PowerOn();
            IDocument doc1;
            IDocument doc2;

            device.Scan(out doc1);
            device.Scan(out doc2);

            Assert.AreEqual(2, device.ScanCounter);
            Assert.IsNotNull(doc1);
            Assert.IsNotNull(doc2);
            Assert.AreEqual("ImageScan0.jpg", doc1.GetFileName());
            Assert.AreEqual("ImageScan1.jpg", doc2.GetFileName());

        }

        [TestMethod]
        public void MultiFunctionalDevice_TestScanAndPrint()
        {
            var device = new MultiFunctionalDevice();
            device.PowerOn();

            device.ScanAndPrint();
            Assert.AreEqual(1, device.ScanCounter);
            Assert.AreEqual(1, device.PrintCounter);
        }

        [TestMethod]
        public void MultiFunctionalDevice_TestSendFax()
        {
            var device = new MultiFunctionalDevice();
            device.PowerOn();

            IDocument doc = new PDFDocument("doc.pdf");
            device.SendFax(in doc, "123456789");
            device.SendFax(in doc, "246810101");
            Assert.AreEqual(2, device.FaxCounter);
        }

        [TestMethod]
        public void MultiFunctionalDevice_TestSendFaxPowerOff()
        {
            var device = new MultiFunctionalDevice();
            device.PowerOff();

            IDocument doc = new PDFDocument("doc.pdf");
            device.SendFax(in doc, "123456789");
            device.SendFax(in doc, "246810101");
            Assert.AreEqual(0, device.FaxCounter);
        }
    }
}
