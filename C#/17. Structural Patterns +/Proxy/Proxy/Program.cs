using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Proxy
{
     interface IStudent
     {
          Student GetStudent(string name);
     }
     class Student 
     {
          public string Name { get; set; }
          public int Age { get; set; }
          public string NameOfTeza { get; set; }
          public Student(string name, int age, string nameOfTeza)
          {
               Name = name;
               Age = age;
               NameOfTeza = nameOfTeza;
          }
          public override string ToString()
          {
               return $"Name: {Name} Age: {Age} Teza: {NameOfTeza}";
          }
     }
     class GroupContext
     {
         public List<Student> Students { get; set; }
          public GroupContext()
          {
               Students = new List<Student>();
          }
     }
     class Group:IStudent
     {
          private GroupContext db;
          public Group()
          {
               db = new GroupContext();
          }
          public Student GetStudent(string name)
          {
               Console.ForegroundColor = ConsoleColor.Red;
               Console.WriteLine("Get student from Server");
               Console.ResetColor();
               return db.Students.FirstOrDefault(s => s.Name == name);
          }
          public void AddStudent(Student st)
          {
               db.Students.Add(st);
          }
     }
     class GroupProxy:IStudent
     {
          private List<Student> ListStudents;
          private Group group;
          public GroupProxy(Group group)
          {
               ListStudents = new List<Student>();
               this.group = group;
          }
          public Student GetStudent(string name)
          {
               Console.ForegroundColor = ConsoleColor.Red;
               Console.WriteLine("Get student from Proxy");
               Console.ResetColor();
               Student student = ListStudents.FirstOrDefault(s => s.Name == name);
               if (student == null)
               {
                    if (group == null)
                         group = new Group();
                    student = group.GetStudent(name);
                    ListStudents.Add(student);
               }
               return student;
          }
     }
     class Program
     {
          static void Main(string[] args)
          {
               Group gr = new Group();
               gr.AddStudent(new Student("Cristian", 12, "MVC"));
               gr.AddStudent(new Student("Angela", 16, "Proxy"));
               gr.AddStudent(new Student("Oleg", 21, "Singleton"));
               IStudent Students = new GroupProxy(gr);
               Console.WriteLine(Students.GetStudent("Cristian"));
               Console.WriteLine(Students.GetStudent("Angela"));
               Console.WriteLine(Students.GetStudent("Cristian"));
               Console.ReadKey();

          }
     }
}
