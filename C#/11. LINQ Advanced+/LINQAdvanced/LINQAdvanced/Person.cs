using System;
public enum TypeOfWork { Student = 4, Worker }
public class Book : IComparable
{
    public string Name { get; set; }
    public string Authour { get; set; }
    public int Pages { get; set; }

    //public Book()
    //{
    //    Name = null;
    //    Authour = null;
    //    Pages = 0;
    //}
    public Book(string name, string authour, int pages)
    {
        Name = name;
        Authour = authour;
        Pages = pages;
    }
    public override string ToString()
    {
        return $"\n\t Name: {Name}, Authour: {Authour} , Pages {Pages}";
    }

    public int CompareTo(object obj)
    {
        return this.Pages.CompareTo(((Book)obj).Pages);
    }
}
public class Person
{
    public static int NumberofPerson = 1;
    public string Name { get; set; }
    public string SurName { get; set; }
    public int? Id { get; set; }
    public int? Age { get; set; }
    public TypeOfWork? Work { get; set; }
    public LinkedList<Book>Books { get; set; }
    //public Person()
    //{
    //    Name = "";
    //    SurName = "";
    //    Id = null;
    //    Work = null;
    //    Books = null;
    //    Age = null;
    //}
    public Person(string name, string surName, TypeOfWork work, int age, LinkedList<Book> books)
    {
        Name = name;
        SurName = surName;
        Work = work;
        Id = NumberofPerson;
        NumberofPerson++;
        Age = age;
        Books = books;
    }
    public override string ToString()
    {
        return $"Name: {Name}, SurName: {SurName}, Age {Age} , TypeOfWork: {Work.ToString()}, Id: {Id} {Books}";
    }
}