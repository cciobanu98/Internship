using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD
{
    public static class Calculator
    {
       public static int Add(int a, int b)
        {
            return a + b;
        }
        public static int Sub(int a, int b)
        {
            return a - b;
        }
        public static int Mult(int a, int b)
        {
            return a * b;
        }
        public static int Div(int a, int b)
        {
            return a / b;
        }
        public static int Pow(int a, int b)
        {
            int pow = 1;
            for (int i = 0; i < b; i++)
                pow *= a;
            return pow;
        }
    }
    public class Product
    {
        public int Price { get; set; }
        public Product(int price)
        {
            Price = price;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }
}
