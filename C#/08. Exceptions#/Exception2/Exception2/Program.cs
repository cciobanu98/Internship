using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exception2
{
     public class DiffError
     {
          private int[] arr = new int[3];
          public void Divide(int a)
          {
               try
               {
                    Console.WriteLine($"Divide: {10 / a}");
               }
               catch (DivideByZeroException e)
               {
                    throw;
               }
          }
          public void GetElement(int n)
          {
               try
               {
                    Console.WriteLine($"Element {arr[n]}");
               }
               catch (IndexOutOfRangeException e)
               {
                    throw e;
               }
          }
          public void MyException(string str)
          {
               try
               {
                    Console.WriteLine(str.Split(' '));
               }
               catch (Exception e)
               {
                    throw new Exception("Send exception ", e);
               }
          }
          public void Error()
          {
               throw new Exception("My exception");
          }
               
     }
     class Program
     {
          static void Main(string[] args)
          {
               DiffError error = new DiffError();
               try
               {
                    //error.Divide(0);
                    //error.GetElement(7);
                    //error.Error();
                    error.MyException(null);
               }
               catch(Exception e)
               {
                    Console.WriteLine(e);
               }
               finally
               {
                    Console.WriteLine("All done");
               }
               Console.ReadKey();
          }
     }
}
