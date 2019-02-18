using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace LinkedList
{
    public enum TypeOfWork { Student = 4, Worker }
    class EqualityPersonComparer : IEqualityComparer<Person>
    {
        public bool Equals(Person x, Person y)
        {
            return x.Work == y.Work && x.SurName == y.SurName && x.Work == y.Work;
        }

        public int GetHashCode(Person x)
        {
            return (x.Work.ToString() + x.Name + x.SurName).GetHashCode();
        }
    }
    public class Person
    {
        public static int NumberofPerson = 1;
        public string Name { get; set; }
        public string SurName { get; set; }
        public int? Id { get; set; }
        public TypeOfWork? Work { get; set; }
        public Person()
        {
            Name = "";
            SurName = "";
            Id = null;
            Work = null;
        }
        public Person(string name, string surName, TypeOfWork work)
        {
            Name = name;
            SurName = surName;
            Work = work;
            Id = NumberofPerson;
            NumberofPerson++;
        }
        public override string ToString()
        {
            return $"Name: {Name}, SurName: {SurName}, TypeOfWork: {Work.ToString()}, Id: {Id}";
        }
    }
    public class Node<T>
    {
        public T Item { get; set; }
        public Node<T> Next { get; set; }
        public Node(T item)
        {
            Item = item;
        }
        public Node()
        {
            Item = default(T);
        }
    }
    public class LinkedList<T> : IEnumerable<T>
    {
        #region Filds and Prop
        public Node<T> Head { get; set; }
        public Node<T> Tail { get; set; }
        public delegate int CompareDelegate(object a, object b);
        private readonly CompareDelegate Comparer;
        public int Count;
        #endregion

        public LinkedList(CompareDelegate comparer)
        {
            Comparer = comparer;
        }
        public LinkedList()
        {

        }
        public void Add(T data)
        {
            Node<T> item = new Node<T>(data);
            if (Head == null)
            {
                Head = item;
                Tail = item;
            }
            else
                Tail.Next = item;
            Tail = item;
            Count++;
        }
        public Node<T> Search(T item)
        {
            Node<T> head = Head;
            while (head != null)
            {
                if (head.Item.Equals(item))
                    return head;
                head = head.Next;
            }
            return null;
        }
        public Node<T> Search(int index)
        {
            if (index >= Count)
                throw new IndexOutOfRangeException("Index out of Range");
            Node<T> head = Head;
            int i = -1;
            while (++i < index)
                head = head.Next;
            return head;
        }
        public bool Swap(T item1, T item2)
        {
            Node<T> item1ref = Search(item1);
            Node<T> item2ref = Search(item2);
            if (item1ref == null || item2ref == null)
                return false;
            item1ref.Item = item2;
            item2ref.Item = item1;
            return true;
        }
        public bool Swap(int index1, int index2)
        {
            Node<T> item1ref = Search(index1);
            Node<T> item2ref = Search(index2);
            if (item1ref == null || item2ref == null)
                return false;
            T temp = item1ref.Item;
            item1ref.Item = item2ref.Item;
            item2ref.Item = temp;
            return true;
        }
        public bool Swap<T1>(int index1, int index2)
        {
            Node<T> item1ref = Search(index1);
            Node<T> item2ref = Search(index2);
            if (item1ref == null || item2ref == null)
                return false;
            T temp = item1ref.Item;
            item1ref.Item = item2ref.Item;
            item2ref.Item = temp;
            return true;
        }
        public bool Remove(T data)
        {
            if (Head.Item.Equals(data))
            {
                Head = Head.Next;
                Count--;
                return true;
            }
            else
            {
                Node<T> head = Head;
                while (head.Next != null)
                {
                    if (head.Next.Item.Equals(data))
                    {
                        head.Next = head.Next.Next;
                        Count--;
                        return true;
                    }
                    head = head.Next;
                }
            }
            return false;
        }
        public void Print()
        {
            Node<T> head = Head;
            while (head != null)
            {
                Console.WriteLine(head.Item);
                head = head.Next;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> head = Head;
            while(head != null)
            {
                yield return head.Item;
                head = head.Next;
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
        public void Sort()
        {
            bool swapped = false;
            Node<T> n1, n2 = null, head = Head;
            if (head == null)
                return;
            do
            {
                swapped = false;
                n1 = head;
                while(n1.Next != n2)
                {
                    if (Comparer(n1.Item, n1.Next.Item) > 0)
                    {
                        Swap(n1.Item, n1.Next.Item);
                        swapped = true;
                    }
                    n1 = n1.Next;
                }
                n2 = n1;
            } while (swapped);
        }
        public void Sort(CompareDelegate comparer)
        {
            bool swapped = false;
            Node<T> n1, n2 = null, head = Head;
            if (head == null)
                return;
            do
            {
                swapped = false;
                n1 = head;
                while (n1.Next != n2)
                {
                    if (comparer(n1.Item, n1.Next.Item) > 0)
                    {
                        Swap(n1.Item, n1.Next.Item);
                        swapped = true;
                    }
                    n1 = n1.Next;
                }
                n2 = n1;
            } while (swapped);
        }
        public void Sort(Comparison<T> comparer)
        {
            bool swapped = false;
            Node<T> n1, n2 = null, head = Head;
            if (head == null)
                return;
            do
            {
                swapped = false;
                n1 = head;
                while (n1.Next != n2)
                {
                    if (comparer(n1.Item, n1.Next.Item) > 0)
                    {
                        Swap(n1.Item, n1.Next.Item);
                        swapped = true;
                    }
                    n1 = n1.Next;
                }
                n2 = n1;
            } while (swapped);
        }
        public T this[int index]
        {
            get
            {
                if (index >= Count || index < 0)
                    throw new IndexOutOfRangeException("Index out of Range");
                else
                    return Search(index).Item;
            }
            set
            {
                if (index >= Count || index < 0)
                    throw new IndexOutOfRangeException("Index out of Range");
                else
                    Search(index).Item = value;
            }
        }

    }
    public static class ExtensionsToLinkedList
    {
        public static void PrintExtension<T>(this LinkedList<T> p)
        {
            Console.WriteLine("Print Extension Method");
        }
        public static void PrintString(this String s)
        {
            Console.WriteLine($"String Extenions {s}");
        }
    }
    class Program
    {
     
        static LinkedList<Person> GetListofPerson(LinkedList<Person>.CompareDelegate deleg)
        {
            LinkedList<Person> listPerson = new LinkedList<Person>(deleg);
            listPerson.Add(new Person("Cristian", "Ciobanu", TypeOfWork.Student));
            listPerson.Add(new Person("Angela", "Beregoi", TypeOfWork.Student));
            listPerson.Add(new Person());
            listPerson.Add(new Person("Veceslav", "Mocanu", TypeOfWork.Worker));
            listPerson.Add(new Person());
            listPerson.Add(new Person("Oleg", "Baranov", TypeOfWork.Student));
            return listPerson;
        }
        static LinkedList<Person> GetListofPerson()
        {
            LinkedList<Person> listPerson = new LinkedList<Person>();
            listPerson.Add(new Person("Cristian", "Ciobanu", TypeOfWork.Student));
            listPerson.Add(new Person("Angela", "Beregoi", TypeOfWork.Student));
            listPerson.Add(new Person());
            listPerson.Add(new Person("Veceslav", "Mocanu", TypeOfWork.Worker));
            listPerson.Add(new Person());
            listPerson.Add(new Person("Oleg", "Baranov", TypeOfWork.Student));
            return listPerson;
        }
        public static int CompareByName(object a, object b)
        {
            return ((Person)a).Name.CompareTo(((Person)b).Name);
        }
        public static void printList(LinkedList<Person> lst, string txt)
        {
            Console.WriteLine("".PadLeft(100, '*'));
            Console.WriteLine(txt);
            foreach (Person p in lst)
                Console.WriteLine(p);
            Console.WriteLine("".PadLeft(100, '*'));
        }
        static void Main(string[] args)
        {
            LinkedList<Person> lst = GetListofPerson(CompareByName);
            printList(lst, "Initial list");
            lst.Sort();
            printList(lst, "Sorted list");
            LinkedList<Person> lst2 = GetListofPerson(delegate(object a, object b)
            {
                return ((Person)a).SurName.CompareTo(((Person)b).SurName);
            });
            printList(lst2, "Initial list");
            lst2.Sort();
            printList(lst2, "Sorted by Delegate");
            LinkedList<Person> lst3 = GetListofPerson((a, b) =>
            {
                return ((Person)a).SurName.CompareTo(((Person)b).SurName);
            });
            printList(lst3, "Initial list");
            lst3.Sort((object a, object b) => ((Person)a).Name.CompareTo(((Person)b).Name));
            lst.Sort((Person a, Person b) => (a.Name.CompareTo(b.Name)));
            lst3.Sort();
            printList(lst3, "Sorted by Anonymus/Lambda ");
            var select = from p in lst
                         where p.SurName.Contains('B')
                         orderby p.Name ascending
                         select p;
            lst.Add(new Person("Valentin", "Butnaru", TypeOfWork.Worker));
            foreach(var s in select)
                 Console.WriteLine(s);
            var select2 = lst.Where(p => p.Work == TypeOfWork.Student)
                .OrderBy(p => p.Name)
                .Select(p => $"{p.Name} {p.SurName}");
            Console.WriteLine("".PadLeft(100, '*'));
            Console.WriteLine("Student Person");
            foreach (var s in select2)
                Console.WriteLine(s);
            Console.WriteLine("".PadLeft(100, '*'));
            lst.PrintExtension();
            string t = "MyExtension";
            t.PrintString();
            Console.ReadKey();
        }
    }
}
