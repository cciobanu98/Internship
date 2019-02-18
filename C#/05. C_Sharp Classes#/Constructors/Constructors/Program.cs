using System;

class Base
{
    public Base()
    {
        Console.WriteLine("Base constructor");
    }
    public Base(string s)
    {
        Console.WriteLine(s);
    }
}
class Kid1:Base
{
    public Kid1():base()
    {
        Console.WriteLine("Kid 1 constructor");
    }
}
class Kid2:Kid1
{
    public Kid2():base()
    {
        Console.WriteLine("Kid 2 constructor");
    }
    public Kid2(string s):base()
    {
        Console.WriteLine("parameters");
    }
}
class Kid3:Kid2
{
    public Kid3():base("Constructor parameters")
    {
        Console.WriteLine("Kid 3 constructor");
    }
    
}
  
namespace Constructors
{
    class Program
    {
        static void Main(string[] args)
        {
            Kid3 kid = new Kid3();
            Console.ReadKey();
        }
    }
}
