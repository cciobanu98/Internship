using System;
using System.Collections.Generic;
namespace Features
{
    class DynamicClass
    {
        public dynamic D { get; set; } = 42;
        public override string ToString()
        {
            return D.ToString();
        }
    }
    class SympliMath
    {
        public int A { get; set; }
        public int B { get; set; }
        public SympliMath(int a, int b)
        {
            A = a;
            B = b;
        }
        public int Sum => A + B;
        public int Diff() => A - B;
        public int Mult() => A * B;
    }
    class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public Person(string name, int age)
        {
            Age = age;
            Name = name ?? throw new Exception($"{nameof(name)} is null");
        }
        public override string ToString()
        {
            return Name + Age.ToString();
        }
    }
    class Program
    {
        static void WriteWithColor(string message, ConsoleColor color = ConsoleColor.Red, int n = 1)
        {
            Console.BackgroundColor = color;
            for(int i=0; i< n; i++)
                Console.WriteLine($"{message}, Iteration: {i}");
            Console.ResetColor();
        }
        static void DynaminExperiment(dynamic d)
        {
            Console.WriteLine($"This is dynamic {d}, type: {d.GetType()}");
        }
        static void TestingOut(out int t)
        {
            t = 42;
        }
        static void TestingTuples()
        {
            var digit = (1, 2, "3", Tom: new Person("Tom", 23));
            Console.WriteLine(digit);
            Console.WriteLine(digit.Tom);
        }
        static (int , int , int ) myMath(int a, int b)
        {
            return (a + b, a - b, a * b);
        }
        static void TestDictonary()
        {
            Dictionary<int, string> dict = new Dictionary<int, string>()
            {
                [0] = "Index 0",
                [1] = "Index 1",
                [2] = "Index 2"

            };
        }
        static void Main(string[] args)
        {
            dynamic d = new DynamicClass();
            List<dynamic> dynamicLst = new List<dynamic>();
            dynamicLst.Add(d);
            dynamicLst.Add("42");
            dynamicLst.Add(42);
            WriteWithColor("Test", n: 3);
            WriteWithColor("Test2", ConsoleColor.DarkGray);
            WriteWithColor("Test3", n: 2, color: ConsoleColor.DarkMagenta);
            Console.WriteLine();
            DynaminExperiment(42);
            DynaminExperiment("42");
            DynaminExperiment(d);
            d = 23;
            DynaminExperiment(d);
            foreach (var item in dynamicLst)
                DynaminExperiment(item);
            Console.WriteLine();
            Console.WriteLine((new SympliMath(2, 3)).Sum);
            Console.WriteLine((new SympliMath(2, 3)).Mult());
            Console.WriteLine();
            try
            {
                Person p = new Person(null, 12);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            TestingOut(out int myVar);
            Console.WriteLine($"Out var: {myVar}");
            TestingTuples();
            int mult;
            Console.WriteLine($"MyMath is {(_, _, mult) = myMath(2, 3)}");
            Console.WriteLine($"Multiplication {mult}");
            Console.ReadKey();
        }
    }
}
