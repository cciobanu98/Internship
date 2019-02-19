using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace LinkedList
{
    class Program
    {
        [MethodImpl(MethodImplOptions.NoInlining)]
        public static string GetCurrentMethod()
        {
            var st = new StackTrace();
            var sf = st.GetFrame(1);

            return sf.GetMethod().Name;
        }
        public static LinkedList<Book> GetBooks()
        {
            LinkedList<Book> books = new LinkedList<Book>(new Book("GOT", "Martin", 1200),
              new Book("Clash of Kings", "Martin", 900),
              new Book("Luceafarul", "Eminescu", 100),
              new Book("Amintiri din copilarie", "Creanga", 456),
              new Book("Floarea Albastra", "Eminescu", 120),
              new Book("Harry Potter", "Rowling", 1500),
              new Book("Hobbit", "Tolkien", 1600),
              new Book("LOTR", "Tolkien", 1300),
              new Book("American Goods", "SameAuthor", 1450),
              new Book("Fratii Jderi", "Sadoveanu", 450),
              new Book("Povara bunatatii noastre", "Druta", 140),
              new Book("Padurea Spinzuratilor", "Druta", 170),
              new Book("Harry Potter version 2", "Rowling", 1400),
              new Book("Fantastic beats", "Rowling", 1200),
              new Book("Nu conteaza", "Baranov", 1),
              new Book("MyBook","Ciobanu",666),
              new Book("Nu conteaza 2", "Baranov", 412));
            return books;
        }
        //public static LinkedList<Person> GetListofPerson(LinkedList<Person>.CompareDelegate deleg)
        //{
        //    LinkedList<Person> listPerson = new LinkedList<Person>(deleg);
        //    listPerson.Add(new Person("Cristian", "Ciobanu", TypeOfWork.Student, 20, new LinkedList<Book>(books[2], books[1], books[13], books[0], books[3])));
        //    listPerson.Add(new Person("Angela", "Beregoi", TypeOfWork.Student, 19, new LinkedList<Book>(books[0], books[4], books[5], books[9])));
        //    listPerson.Add(new Person("Veceslav", "Mocanu", TypeOfWork.Worker, 22, new LinkedList<Book>(books[6], books[8], books[7], books[5], books[13])));
        //    listPerson.Add(new Person("Oleg", "Baranov", TypeOfWork.Student, 21, new LinkedList<Book>(books[10], books[11], books[12], books[0], books[1])));
        //    return listPerson;
        //}
        public static LinkedList<Person> GetListofPerson(LinkedList<Book> books)
        {
            LinkedList<Person> listPerson = new LinkedList<Person>();
            listPerson.Add(new Person("Cristian", "Ciobanu", TypeOfWork.Student, 20, new LinkedList<Book>(books[2], books[1], books[13], books[0], books[3])));
            listPerson.Add(new Person("Angela", "Beregoi", TypeOfWork.Student, 19, new LinkedList<Book>(books[0], books[4], books[5], books[9])));
            listPerson.Add(new Person("Veceslav", "Mocanu", TypeOfWork.Worker, 22, new LinkedList<Book>(books[6], books[8], books[7], books[5], books[13])));
            listPerson.Add(new Person("Oleg", "Baranov", TypeOfWork.Student, 21, new LinkedList<Book>(books[10], books[11], books[12], books[0], books[1])));
            return listPerson;
        }
        public static void printList(LinkedList<Person> lst, string txt)
        {
            Console.WriteLine("".PadLeft(100, '*'));
            Console.WriteLine(txt);
            foreach (Person p in lst)
                Console.WriteLine(p);
            Console.WriteLine("".PadLeft(100, '*'));
        }
        public static void WhereMethod(LinkedList<Person> lst)
        {
            Console.WriteLine(GetCurrentMethod());
            var s = from p in lst
                    where (p.SurName.Contains("B"))
                    select p;
            foreach (var item in s)
                Console.WriteLine(item);
        }
        public static void TakeMethod(LinkedList<Person> lst)
        {
            Console.WriteLine(GetCurrentMethod());
            var s = lst.Take(2);
            foreach (var item in s)
                Console.WriteLine(item);
        }
        public static void SelectMany(LinkedList<Person> lst)
        {
            Console.WriteLine(GetCurrentMethod());
            var s = lst.SelectMany(x => x.Books);
            var s2 = from p in s
                     where (p.Pages < 1000)
                     select p;
            foreach (var item in s2)
                Console.Write(item);
        }
        public static void JoinMethod(LinkedList<Person> lst, LinkedList<Book> books)
        {
            Console.WriteLine(GetCurrentMethod());
            var s = lst.Join(books,
                person => person.SurName,
                book => book.Authour,
                (person, book) => new {Name =  person.Name, SurName = person.SurName, Book = book.Name}
                );
            foreach (var item in s)
                Console.WriteLine($"{item.Name} {item.SurName} {item.Book} ");
        }
        public static void GroupJoinMethod(LinkedList<Person> lst, LinkedList<Book> books)
        {
            Console.WriteLine(GetCurrentMethod());
            var s = lst.GroupJoin(books,
                person => person.SurName,
                book => book.Authour,
                (person, book) => new { Name = person.Name, SurName = person.SurName, Book = book }
                );
            foreach (var item in s)
            {
                Console.WriteLine($"{item.Name} {item.SurName} ");
                foreach (var item2 in item.Book)
                    Console.Write($"\t{item2}");
                Console.WriteLine();
            }
        }
        public static void OrderByMethod(LinkedList<Book> books)
        {
            Console.WriteLine(GetCurrentMethod());
            var s = from b in books
                    where (b.Pages < 1000)
                    orderby b.Name ascending
                    select b;
            foreach (var item in s)
                Console.WriteLine(item);
        }
        public static void GroupByMethod(LinkedList<Book> books)
        {
            Console.WriteLine(GetCurrentMethod());
            var s = books.GroupBy(
                book => book.Authour,
                book2 => book2,
                (Authour, book2) => new { name = Authour , Books = book2});
            foreach (var item in s)
            {
                Console.WriteLine(item.name);
                foreach (var item2 in item.Books)
                    Console.Write(item2);
                Console.WriteLine();
            }
        }
        static void Main(string[] args)
        {
            LinkedList<Book> books = GetBooks();
            LinkedList<Person> lst = GetListofPerson(books);
            Console.Write(lst);
           // GroupByMethod(books);
            Console.WriteLine(books.Max());
            Console.ReadKey();
        }
    }
}
