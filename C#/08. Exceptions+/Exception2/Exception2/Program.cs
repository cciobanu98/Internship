using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exception2
{
    public class DiffError
    {
        private int[] arr = new int[3];
        public void Divide(int a)
        {
            try
            {
                test(a);
            }
            catch (DivideByZeroException e)
            {
                throw;
            }
        }

        private static void test(int a)
        {
            Console.WriteLine($"Divide: {10 / a}");
        }

        public void GetElement(int n)
        {
            try
            {
                NewMethod(n);
            }
            catch (IndexOutOfRangeException e)
            {
                throw e;
            }
        }

        private void NewMethod(int n)
        {
            Console.WriteLine($"Element {arr[n]}");
        }

        public void MyException(string str)
        {
            try
            {
                Console.WriteLine(str.Split(' '));
            }
            catch (Exception e)
            {
                throw new Exception("Send exception ", e);
            }
        }
        public void Error()
        {
            throw new Exception("My exception");
        }

    }
#if RELEASE
    class T { }
#endif
    class Program
    {
        static void Main(string[] args)
        {
            DiffError error = new DiffError();
            try
            {
                //error.Divide(0);
                //error.GetElement(7);
                //error.Error();
                error.MyException(null);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                Console.WriteLine("All done");
            }
            Console.ReadKey();
        }
    }
}
