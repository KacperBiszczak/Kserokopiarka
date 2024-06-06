namespace Zadanie2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var md = new MultiFunctionalDevice();

            IDocument doc1 = new PDFDocument("abcdef.pdf");
            IDocument doc2;

            md.PowerOn();
            md.Print(doc1);
            md.Scan(out doc2);
            md.Scan(out doc2);
            md.SendFax(doc1, "604392849");
            md.SendFax(doc1, "235432904");
            md.SendFax(doc1, "493208234");
            
            md.PowerOff();
            md.SendFax(doc2, "654632454");

            Console.WriteLine("Print counter: " + md.PrintCounter);
            Console.WriteLine("Scan counter: " + md.ScanCounter);
            Console.WriteLine("Fax counter: " + md.FaxCounter);
            Console.WriteLine("Turning on/off counter: " + md.Counter);
        }
    }
}
