using System.Threading;
using System;

class Program
{
    static void Main(string[] args)
    {
        Thread myThread = new Thread(new ThreadStart(Count));
        myThread.Start();

        for (int i = 1; i < 9; i++)
        {
            Console.WriteLine(i * i);
            Thread.Sleep(300);
        }

        Console.ReadLine();
    }

    public static void Count()
    {
        for (int i = 1; i < 9; i++)
        {
            Console.WriteLine(i * i);
            Thread.Sleep(700);
        }
    }
}