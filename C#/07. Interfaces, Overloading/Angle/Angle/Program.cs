using System;
using System.Collections;
using System.Collections.Generic;
namespace Angle
{
    #region Comparer
    class SortSec : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is AngleType a && y is AngleType b)
                return Compare(a.Sec, b.Sec);
            else
                throw new Exception("Object are not AngleType");
        }
    }
    class MinSec : IComparer
    {
        public int Compare(object x, object y)
        {
            if (x is AngleType a && y is AngleType b)
                return Compare(a.Min, b.Min);
            else
                throw new Exception("Object are not AngleType");
        }
    }
    #endregion
    public class AngleType : IComparable, IEnumerable, ICloneable
    {
        #region Field
        private int angle;
        private int min;
        private int sec;
        private int pos = -1;
        #endregion
        #region Propriety
        public int Angle
        {
            get { return angle; }
            set { angle = value; }
        }
        public int Min
        {
            set
            {
                if (value > 60 || value < 0)
                    throw new Exception("Minuntes can't be more than 60 or less than 0");
                else
                    min = value;
            }
            get
            {
                return min;
            }
        }
        public int Sec
        {
            set
            {
                if (value > 60 || value < 0)
                    throw new Exception("Secundes can't be more than 60 or less than 0");
                else
                    sec = value;
            }
            get
            {
                return min;
            }
        }
        #endregion
        #region Constructors
        public AngleType()
        {
            angle = 0;
            min = 0;
            sec = 0;
        }
        public AngleType(int angle, int min, int sec)
        {
            this.angle = angle;
            this.min = min;
            this.sec = sec;
        }
        #endregion
        #region ToString
        public override string ToString()
        {
            return $@"Angle: {angle}^ {min}' {sec}'' ";
        }
        #endregion
        #region Operator-
        public static AngleType operator+(AngleType a, AngleType b)
        {
            int sec;
            int min;
            int angle;

            sec = (a.sec + b.sec) % 60;
            min = (a.sec + b.sec) / 60 + ((a.min + b.min) % 60);
            angle = (a.min + b.min) / 60 + a.angle + b.angle;
            return new AngleType(angle, min, sec);
        }
        #endregion
        #region Operator-
        public static AngleType operator-(AngleType a, AngleType b)
        {
            int sec = 0;
            int min = 0;
            int angle = 0;

            sec = a.sec - b.sec;
            if (sec < 0)
            {
                sec += 60;
                min--;
            }
            min = min + a.sec - b.sec;
            if (min < 0)
            {
                min += 60;
                angle--;
            }
            angle = angle + a.angle - b.angle;
            return new AngleType(angle, min, sec);
        }
        #endregion
        #region Operator == and !=
        public static  bool operator==(AngleType a, AngleType b)
        {
            if (a.angle == b.angle && a.min == b.min && a.sec == b.sec)
                return true;
            return false;
        }
        public static bool operator !=(AngleType a, AngleType b)
        {
            return !(a == b);
        }
        #endregion
        #region Indexator
        public int this[int i]
        {
            get
            {
                switch(i)
                {
                    case 0:
                        return angle;
                    case 1:
                        return min;
                    case 2:
                        return sec;
                    default:
                        throw new Exception("Invalid index");
                }
            }
            set
            {
                switch(i)
                {
                    case 0:
                        angle = value;
                        break;
                    case 1:
                        min = value;
                        break;
                    case 2:
                        sec = value;
                        break;
                    default:
                        throw new Exception("Invalid index");
                }
            }
        }
        #endregion
        #region Icomparable
        public int CompareTo(object obj)
        {
            if (obj == null)
                return 1;
            AngleType a = obj as AngleType;
            if (a == null)
                throw new Exception("Object is not a Angle");
            else
                return this.angle.CompareTo(a.angle);
        }
        #endregion
        #region IEnumerable with yield
        public  IEnumerable<int> AngleValue
        {
            get
            {
                yield return angle;
                yield return min;
                yield return sec;
            }
        }
        #endregion
        #region IEnumerable
        public IEnumerator GetEnumerator()
        {
            return new AngleEnumerator(this);
        }
        #endregion
        #region IEnumerator
        private class AngleEnumerator:IEnumerator
        {
            int pos = -1;
            AngleType t;
            public AngleEnumerator(AngleType t)
            {
                this.t = t;
            }
            public bool MoveNext()
            {
                pos++;
                return (pos < 3);
            }
            public void Reset()
            {
                pos = -1;
            }
            public object Current
            {
                get
                {
                    switch (pos)
                    {
                        case 0:
                            return t.Angle;
                        case 1:
                            return t.Min;
                        case 2:
                            return t.Sec;
                        default:
                            throw new Exception("Index OutOfRange");
                    }
                }
            }
        }
        #endregion
        #region ICloneable
        public object Clone()
        {
            return (AngleType)this.MemberwiseClone();
        }
        #endregion
    }
    class Program
    {
        static void Main(string[] args)
        {
            AngleType a = new AngleType(20, 30, 40);
            AngleType b = new AngleType(11, 21, 31);
            AngleType[] t = { new AngleType(1, 1, 1), new AngleType(2, 2, 2), new AngleType(3, 3, 3), new AngleType(4, 4, 4) };
            Console.WriteLine(a);
            Console.WriteLine(b);
            Console.WriteLine($"Add: {a + b}");
            Console.WriteLine($"Diff: {a - b}");
            Console.WriteLine($"Equal: {a == b}");
            Console.WriteLine($"!Equal: {a != b}");
            Console.WriteLine($"Acces by index: {a[0]}, {a[1]}, {a[2]}");
            Console.WriteLine("Foreach acces");
            foreach (int i in a)
                Console.Write(i + " ");
            Console.WriteLine("\nForeach acces with yield ");
            foreach (int i in a.AngleValue)
                Console.Write(i + " ");
            Console.WriteLine("\nClone");
            AngleType c = (AngleType)a.Clone();
            Console.WriteLine(c);
            Console.WriteLine("Array foreach: ");
            foreach (AngleType i in t)
                Console.WriteLine(i);
            a[2] = 2;
            Console.ReadKey();
        }
    }
}
