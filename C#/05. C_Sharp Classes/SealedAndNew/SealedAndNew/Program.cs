using System;

class Base
{
    public virtual void WriteMethod()
    {
        Console.WriteLine("This is Base");
    }
}
class Kid:Base
{
    public  override void WriteMethod()
    {
        Console.WriteLine("This is Kid");
        //base.WriteMethod();
    }
}
class Kid2:Kid
{
    public new void WriteMethod()
    {
        Console.WriteLine("This is kid2");
    }
}
class Test
{
    public virtual void WriteTest()
    {
        Console.WriteLine("This is Test Base");
    }
}
class Test2 : Test
{
    public override  void WriteTest()
    {
        Console.WriteLine("This is Derived test");
    }
}
namespace SealedAndNew
{
    class Program
    {
        static void Main(string[] args)
        {
            //Base b = new Base();
            //b.WriteMethod();
            //Base k = new Kid();
            //k.WriteMethod();
            //Base k2 = new Kid2();
            //k2.WriteMethod();
            //Kid2  k22 = new Kid2();
            //k22.WriteMethod();
            Base k222 = new Kid2();
            k222.WriteMethod();
            Test t = new Test2();
            t.WriteTest();
            Console.ReadKey();
        }
    }
}
