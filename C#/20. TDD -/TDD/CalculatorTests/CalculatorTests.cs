

using TDD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace TDD.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [TestCase(2, 3, 6)]
        [TestCase(0,2,0)]
        [TestCase(1, 1, 1)]
        public void MultTest(int a, int b, int rez)
        {
            int calc = Calculator.Mult(a, b);
           Assert.AreEqual(calc, rez);
        }
        [TestCase(2, 3, 5)]
        [TestCase(0, 2, 2)]
        [TestCase(1, 1, 2)]
        public void AddTest(int a, int b, int rez)
        {
            int calc = Calculator.Add(a, b);
            Assert.AreEqual(calc, rez);
        }
        [TestCase(2, 3, -1)]
        [TestCase(0, 2, -2)]
        [TestCase(1, 1, 0)]
        public void SubTest(int a, int b, int rez)
        {
            int calc = Calculator.Sub(a, b);
            Assert.AreEqual(calc, rez);
        }
        [TestCase(2, 3, 0)]
        [TestCase(0, 2, 0)]
        [TestCase(1, 1, 1)]
        public void DivTest(int a, int b, int rez)
        {
            int calc = Calculator.Div(a, b);
            Assert.AreEqual(calc, rez);
        }
        [TestCase(2, 3, 8)]
        [TestCase(0, 2, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(5, 0, 1)] 
        public void PowTest(int a, int b, int rez)
        {
            int calc = Calculator.Pow(a, b);
            Assert.AreEqual(calc, rez);
        }
    }
    [TestFixture]
    public class ProductTests
    {
        Product p;
        bool result;
        [SetUp]
        public void TestSetUp()
        {
            p = new Product(100);
            result = false;
        }
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(-1)]
        [TestCase(-200)]
        [TestCase(200)]
        public void ValidPrice(int i)
        {
            p.Price = i;
            if (p.Price > 0)
                result = true;
            Assert.IsTrue(result, "Price is valid");
        }
        [TearDown]
        public void TestTearDown()
        {
            p = null;
            result = false;
        }
    }
}