using System;

namespace Exceptions
{
    class InvalidIndexException:Exception
    {
        public InvalidIndexException()
        {

        }
        public InvalidIndexException(string name):base(String.Format("Invalid index {0}", name))
        {

        }
    }
    class ExceptionTest
    {

        private int id;
        private string[] Names = { "name1", "name2" , "name 3"};
        public string Name { get; set; }
        public static string str
        {
            set
            {
                throw new Exception("Bred");
            }
        }
        static ExceptionTest()
        {
            str = "200000";
            Console.WriteLine("Static constructor");
            //throw new Exception("Exception in static constructor");
        }
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                if (value < 0)
                    throw new InvalidIndexException("Id can be less than 0");
                else
                    id = value;
            }
        }
        public string this[int id]
        {
            get
            {
                try
                {
                    return Names[id];
                }
                catch (IndexOutOfRangeException e)
                {
                    throw;
                }
                catch(Exception e)
                {
                    throw new Exception("Add something", e);
                }
            }
        }
        public virtual void MethodA()
        {
            try
            {
                MethodB();
                MethodC();
            }
            catch (Exception e)
            {
                MethodC();
                throw;
            }
        }
        public void MethodB()
        {
            throw new Exception("Method B");
        }
        public void MethodC()
        {
            throw new Exception("Method C");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ExceptionTest et = new ExceptionTest();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                try
                {
                    ExceptionTest et2 = new ExceptionTest();
                }
                catch (Exception e2)
                {
                    Console.WriteLine(e2.StackTrace);
                }
            }
            Console.ReadKey();
            //Console.WriteLine("Hello World!");
        }
    }
}
