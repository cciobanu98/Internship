using System;
using System.Text;
using System.Collections.Generic;
namespace Array_String
{
    class Program
    {
        private static int[] GetArray(int n, int lower, int upper)
        {
            Random rnd = new Random();
            List<int> list = new List<int>();
            for (int ctr = 1; ctr <= n; ctr++)
                list.Add(rnd.Next(lower, upper + 1));

            return list.ToArray();
        }
        static void Main(string[] args)
        {
            char[] ch = { 'H', 'e', 'l','l','o' };
            string str1 = new string(ch);
           // str1[2] = 'B';
            Console.WriteLine($"String from array of chars: {str1} ");
            string str2 = "World!";
            Console.WriteLine($"String assignments: {str2}");
            string str3 = str2;
            str3 = "New World";
            str2 = str3;
            Console.WriteLine($"String not  changes: {str3}, {str2}");
            string str4 = "To clone";
            str2 = (string)str4.Clone();
            Console.WriteLine($"Clone Method: {str2}");
            str2 = "AAAAAAA";
            Console.WriteLine($"CompareTo method: {str2.CompareTo(str4)}");
            string str5 = "hello";
            Console.WriteLine($"CompareOrdinal Method: {string.CompareOrdinal(str1, str5)}");
            Console.WriteLine($"Compare Method: {string.Compare(str1, str5)}");
            Console.WriteLine($"Concat Method: {string.Concat(str1, str5)}");
            Console.WriteLine($"+ concat: {str1 + str5}");
            Console.WriteLine($"Contains methos: {str1.Contains(str5)}");
            Console.WriteLine("Copy :{0}", string.Copy(str1));
            char[] dest= { 'D', 'e', 's', 't', 'i', 'n', 'a', 't', 'o', 'n' };
            str1.CopyTo(0, dest, 2, 4);
            Console.WriteLine($"CopyTo method: {new string(dest)}");
            Console.WriteLine($"EndWith : {str4.EndsWith('c')}");
            Console.WriteLine($"Hashcode : {str4.GetHashCode()}");
            Console.WriteLine($"Trim: { str1.Trim('o')}");
            Console.WriteLine($"ToCharArray: {str1.ToCharArray()}");
            int[][] jagged = new int[5][];
            // int[] a = new int[] { 1, 2, 3 };
           // int[] b = new int[5];
            for(int i=0;i<5;i++)
            {
                jagged[i] = new int[i + 1];
                for (int j = 0; j < jagged[i].Length; j++)
                {
                    jagged[i][j] = i + j;
                    Console.Write(jagged[i][j]);
                }
                Console.WriteLine();
            }
            int[][] jagged2 = { new int[] { 1, 2, 3 }, new int[] { 1, 2, 3 } };
            StringBuilder strB = new StringBuilder("test");
            Console.WriteLine("String Builder {0}:", strB);
            strB[2] = 'f';
            Console.WriteLine("String Builder {0}:", strB);
            strB.Append("Append");
            Console.WriteLine("String Builder {0}:", strB);
            int[] values = GetArray(20, 0, 1000);
            for (int i = 0; i < values.Length; i++)
                Console.Write(" " + values[i]);
            int[] find = Array.FindAll(values, x => x < 10 && x > 20);
            for (int i = 0; i < find.Length; i++)
                Console.Write(" " + find[i]);
            Console.WriteLine();
            
            string bigstr = @"Test for gig str
                               New line
                               Other line";
            Console.WriteLine(bigstr);
            string[] split = bigstr.Split('\n');
            foreach (string s in split)
                Console.WriteLine($" Split: {s}");
            string [,] arrStr = new string[,]{ { "Test1", "test2"}, { "test3", "test4"} };
            Console.WriteLine(arrStr);
            foreach (string s1 in arrStr)
                Console.Write(" " + s1);
            Console.WriteLine();
            Console.WriteLine(5 + "5");
            Console.WriteLine(split + "5");
            string s3 = split + "5";
            Console.ReadKey();
        }
    }
}
