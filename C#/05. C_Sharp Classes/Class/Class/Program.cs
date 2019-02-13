using System;
interface IProduct
{
    string Product
    {
        get; set; 
    }
}
class Base:IProduct
{
    public const double PI = 3.14;
    public string Name { get; set; }
    private int id;
    private string pass = "def";
    protected string product;
    public virtual string Product
    {
        get { return product; }
        set { product = value; }
    }
    public int Time
    {
        get { return DateTime.Now.Year; }
    }
    public string Pass
    {
        set
        {
            if (value.Length > 9)
                pass = value;
            else
                pass = "Def123456789";
        }
    }
    public int Id
    {
        get
        {
            if (id < 0)
                return 0;
            return id;
        }
        private set
        {
            if (value > 100)
                id = 100;
            else
                id = value;
        }
    }
    public Base(string name, int id)
    {
        Name = name;
        Id = id;
    }
    public Base()
    {
        Name = "Name";
        Id = 0;
       // pass = "Def123";
        product = "product";
    }
    public virtual void BaseWrite()
    {
        Console.WriteLine("Name:" + Name);
        Console.WriteLine("Id:" + id);
        Console.WriteLine("Pass:" + pass);
        Console.WriteLine("Year: " + Time);
        Console.WriteLine("Product: " + Product);
        Console.WriteLine("Pi: " + PI);
    }
}
class Kid:Base
{
    public override string Product { get => base.Product; set { product = "Kid"; } }
    public sealed override void BaseWrite()
    {
        Console.WriteLine("This is kid");
        base.BaseWrite();
    }
}
class Kid2:Kid
{
    public new void BaseWrite()
    {
        Console.WriteLine("This is Kid2");
    }
}
namespace Class
{
    class Program
    {
        static void Main(string[] args)
        {
            Base b = new Base();
            Base k = new Kid();
            Base k2 = new Kid2();
            k.Product =  "sssss";
           // b.Pass = "Test123";
            //b.BaseWrite();
            //k.BaseWrite();
            k2.BaseWrite();
            Console.ReadKey();
        }
    }
}
