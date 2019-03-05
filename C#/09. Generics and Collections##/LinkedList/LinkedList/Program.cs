using System;
using System.Collections.Generic;
namespace LinkedList
{
     public enum TypeOfWork { Student = 4, Worker }
     class EqualityPersonComparer:IEqualityComparer<Person>
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
        public T Item { get; set;  }
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
        public int Count;
        #endregion
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
            while(head != null)
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
            while(head != null)
            {
               Console.WriteLine(head.Item);
               head = head.Next;
            }
        }
        public IEnumerator<T> GetEnumerator()
        {
            Node<T> head = Head;
            while (head != null)
            {
                yield return head.Item;
                head = head.Next;
            }

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
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
    class Program
    {
          static void TestingDictonary1(LinkedList<Person> listPerson)
          {
               Dictionary<int, Person> PersonDictonary = new Dictionary<int, Person>();
               for (int i = 0; i < listPerson.Count; i++)
                    if (listPerson[i].Id != null)
                         PersonDictonary.Add((int)listPerson[i].Id, listPerson[i]);
               Person Id2;
               if (PersonDictonary.TryGetValue(2, out Id2))
                    Console.WriteLine(Id2);
               if (!PersonDictonary.TryAdd((int)Id2.Id, Id2))
                    Console.WriteLine("Fail to add");
          }
          static void TestingDictonary2(LinkedList<Person> listPerson)
          {
               Dictionary<Person, string> PersonDictonary2 = new Dictionary<Person, string>(new EqualityPersonComparer());
               for (int i = 0; i < listPerson.Count; i++)
                    if (listPerson[i].Id != null)
                         PersonDictonary2.Add(listPerson[i], listPerson[i].Name);
               Person p1 = new Person("Angela", "Beregoi", TypeOfWork.Student);
               Console.WriteLine(listPerson[1]);
               if (p1 != listPerson[1]) 
                    Console.WriteLine("Different refference");
               if (PersonDictonary2.ContainsKey(p1))
                    Console.WriteLine("Person Exist");
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
          static void TestingNulInt()
          {
               LinkedList<int?> list2 = new LinkedList<int?>();
               int[] arr = new int[21];
               for (int i = 0; i < 20; i++)
                    if (i % 3 == 0)
                         list2.Add(null);
                    else
                         list2.Add(i);
               for (int i = 0; i < 20; i++)
                    arr[i] = list2[i] ?? -1;
               foreach (int k in arr)
                    Console.Write(k + " ");
          }
          static void TestingString()
          {
               LinkedList<string> list = new LinkedList<string>();
               for (int i = 0; i < 10; i++)
                    list.Add((i * 3).ToString());
               list.Swap("0", "27");
               list.Print();
          }
          static void TestingInt()
          {
               LinkedList<int> list = new LinkedList<int>();
               for (int i = 0; i < 10; i++)
                    list.Add(i * 3);
               list.Swap(0, 27);
               list.Print();
          }
        static void TestInt2()
        {
            LinkedList<int> list = new LinkedList<int>();
            for (int i = 0; i < 10; i++)
                list.Add(i * 3);
            list.Swap<int>(0, 27);
            list.Print();
        }
        static void Main(string[] args)
        {
               LinkedList<Person> lst = GetListofPerson();
               //TestingString();
               //TestingNulInt();
               //TestingInt();
               //TestingDictonary1(lst);
               //TestingDictonary2(lst);
               //TestInt2();

            Console.ReadKey();
        }
    }
}
