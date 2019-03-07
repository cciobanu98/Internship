using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor
{
    interface IVisitor
    {
        void Visit(Element elem);
    }
    abstract class Element
    {
        public abstract void Accept(IVisitor visitor);
    }
    class Student : Element
    {
        public string Name { get; set; }
        public int Bursa { get; set; }
        public string Work { get; set; }
        public Student(string name, int bursa, string work)
        {
            Name = name;
            Bursa = bursa;
            Work = work;
        }
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
    class ChangeBursa : IVisitor
    {
        public void Visit(Element elem)
        {
            Student st = elem as Student;
            st.Bursa += 100;
            Console.WriteLine($"Student: {st.Name} change bursa by 100");
        }
    }
    class ChangeCompanyToUSM:IVisitor
    {
        public void Visit(Element elem)
        {
            Student st = elem as Student;
            st.Work = "USM";
            Console.WriteLine($"Student: {st.Name} changed to USM");
        }
    }
    class Group
    {
        public List<Student> Lst { get; set; } = new List<Student>();
        public void Add(Student st)
        {
            Lst.Add(st);
        }
        public void Accept(IVisitor visitor)
        {
            foreach (var s in Lst)
                s.Accept(visitor);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Group gr = new Group();
            gr.Add(new Student("Cristian", 100, "Amdaris"));
            gr.Add(new Student("Angela", 200, "UTM"));
            gr.Accept(new ChangeBursa());
            gr.Accept(new ChangeCompanyToUSM());
            Console.ReadKey();
        }
    }
}
