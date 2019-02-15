using System;

namespace LinkedList
{

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
    public class LinkedList<T>
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
        static void Main(string[] args)
        {
            LinkedList<string> list = new LinkedList<string>();
            LinkedList<int> list2 = new LinkedList<int>();
            for (int i = 0; i < 10; i++)
                list.Add((i*3).ToString());
            for (int i = 0; i < 10; i++)
                list2.Add(i);
            list.Swap("0", "27");
            list.Print();
            list2.Swap(0, 27);
            list.Print();
            Console.ReadKey();
        }
    }
}
