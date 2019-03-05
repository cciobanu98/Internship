using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
     abstract class Student
     {
          public string Name { get; set; }
          abstract public void DisplayInfo();
     }
     class ConcretStudent: Student
     {
          public ConcretStudent(string name)
          {
               Name = name;
          }
          public override void DisplayInfo()
          {
               Console.WriteLine($"Name {Name}");
          }
     }
     class StudentDecorator:Student
     {
          protected Student student;
          public StudentDecorator(Student st)
          {
               this.student = st;
          }
          public override void DisplayInfo()
          {
               if (student != null)
                    student.DisplayInfo();
          }
     }
     class StudentBursier:StudentDecorator
     {
          public int Bursa { get; set; }
          public StudentBursier(int bursa, Student st) : base(st)
          {
               Bursa = bursa;
          }
          public override void DisplayInfo()
          {
               base.DisplayInfo();
               Console.WriteLine($"Bursa: {Bursa}");
          }
     }
     class StudentAngajat:StudentDecorator
     {
          public string Company { get; set;}
          public StudentAngajat(string company, Student st):base(st)
          {
               Company = company;
          }
          public override void DisplayInfo()
          {
               base.DisplayInfo();
               Console.WriteLine($"Company: {Company}");
          }
     }

     class Program
     {
          static void Main(string[] args)
          {
               Student st = new ConcretStudent("Cristian");
               st = new StudentBursier(810, st);
               st = new StudentAngajat("Amdaris", st);
               st.DisplayInfo();
               Console.ReadKey();
          }
     }
}
