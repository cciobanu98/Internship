using System;
namespace Basic
{
    class Point
    {
        public int x;
        public int y;
    }
    static class StaticEX
    {
        static DateTime t;
        static StaticEX()
        {
            t = DateTime.Now;
        }
        public static void Print()
        {
            Console.WriteLine(t);
        }
    }
    class Basic
    {
        private void reffTest1(ref int i)
        {
            i += 22;
        }
        private void reffTest2(Point p)
        {
            p.x = 20;
            p.y = 20;
            p = new Point();
        }
        private void reffTest3(ref Point p)
        {
            p = new Point();
        }
        public void outTest1(out int i)
        {
            i = 22;
        }
        public int SendByObject(object b)
        {
            return (int)b;
        }
        public void Test()
        {
            int i = 22;
            Point p = new Point();
            reffTest1(ref i);
            reffTest2(p);
            Console.WriteLine(i);
            Console.WriteLine(p.x + " " + p.y);
            reffTest3(ref p);
            Console.WriteLine(p.x + " " + p.y);
            outTest1(out i);
            Console.WriteLine(i);
            Console.WriteLine(SendByObject(6));
            StaticEX.Print();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Basic b = new Basic();
            b.Test();
            Console.ReadKey();
        }
    }
}
