using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace DisposeFinaliza
{
    class Logger:IDisposable
    {
        private bool disposed = false;
        public Stream Writer { get; set; }
        public Logger(Stream writer)
        {
            Writer = writer;
        }
        public void WriteMessage(string mes)
        {
            Writer.Write(Encoding.ASCII.GetBytes(mes), 0, mes.Length);
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        public virtual void Dispose(bool disposing)
        {
            if(!disposed)
            {
                if(disposing)
                {
                    Console.WriteLine("Disposing Logger");
                    Writer.Dispose();
                }
                disposed = true;
            }
        }
        ~Logger()
        {
            Console.WriteLine("Destructor");
            Dispose(false);
        }
    }
    class Program
    {
        static void TestDestructor()
        {
            Logger logger = new Logger(File.Open("test2.txt", FileMode.OpenOrCreate));
            logger.WriteMessage("My message");
            logger.WriteMessage("Another message");
        }
        static void Main(string[] args)
        {
            TestDestructor();
            Logger logger = new Logger(File.Open("test.txt", FileMode.OpenOrCreate));
            logger.WriteMessage("My message");
            logger.WriteMessage("Another message");
            logger.Dispose();
            Console.ReadKey();
        }
    }
}
