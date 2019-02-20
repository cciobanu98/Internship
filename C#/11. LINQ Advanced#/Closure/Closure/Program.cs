using System;

namespace Closure
{
    delegate void MyDelegate();
    class Program
    {
        public static MyDelegate[] delegates = new MyDelegate[3];
        static void TestClosure()
        {
            int outside = 0;
            for(int i=0;i<2;i++)
            {
                int inside = 0;
                delegates[i] = delegate
                {
                    Console.WriteLine($"{inside} {outside} {i}");
                    outside++;
                    inside++;
                };
                delegates[2] = TestClosure;
            }
        }
        static void Main(string[] args)
        {
            TestClosure();
            MyDelegate del = TestClosure;
            del();
            for (int i = 0; i < 3; i++)
                delegates[i]();
            Console.ReadKey();
        }
    }
}
