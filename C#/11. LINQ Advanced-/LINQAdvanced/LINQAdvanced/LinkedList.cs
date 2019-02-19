using System;
using System.Collections;
using System.Collections.Generic;
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
    public LinkedList(params T[] lst)
    {
        foreach(var l in lst)
        {
            this.Add(l);
        }
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
            while (n1.Next != n2)
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
    public override string ToString()
    {
        string str = "";
        foreach (var l in this)
            str += l.ToString();
        return str + "\n";
    }

}