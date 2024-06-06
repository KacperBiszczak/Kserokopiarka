namespace Zadanie3
{
    public class Program
    {
        static void Main()
        {
            var xerox = new Copier();
            xerox.PowerOn();
            IDocument doc1 = new PDFDocument("aaa.pdf");
            xerox.Print(in doc1);

            IDocument doc2;
            xerox.Scan(out doc2);

            xerox.ScanAndPrint();
            System.Console.WriteLine(xerox.Counter);
            System.Console.WriteLine(xerox.PrintCounter);
            System.Console.WriteLine(xerox.ScanCounter);

            var multiDev = new MultiFunctionalDevice();
            multiDev.PowerOn();
            multiDev.Print(doc1);
            multiDev.Print(doc1);
            multiDev.Scan(out doc1);
            multiDev.Scan(out doc1);
            multiDev.Scan(out doc1);
            multiDev.ScanAndPrint();
            multiDev.SendFax(doc2, "230294290");

            Console.WriteLine(multiDev.Counter);
            Console.WriteLine(multiDev.PrintCounter);
            Console.WriteLine(multiDev.ScanCounter);
            Console.WriteLine(multiDev.FaxCounter);

        }
    }
}
