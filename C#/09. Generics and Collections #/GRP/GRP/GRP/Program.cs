using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRP
{
    interface IRepository<T> where T:class
    {
        void Add(T item);
        void Remove(T item);
        T SearchById(int i);
        //void Update(T item);
        IEnumerable<T> List { get; }
    }
    class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Bursa { get; set; }
        public string University { get; set; }
        public Student(int id, string name, int bursa, string university)
        {
            Name = name;
            Id = id;
            Bursa = bursa;
            University = university;
        }
    }
    class StudentRepository : IRepository<Student>
    {
        public IEnumerable<Student> List
        {
            get
            {
                return students;
            }
        }
        private List<Student> students;
        public StudentRepository()
        {
            students = new List<Student>();
        }
        public void Add(Student item)
        {
            students.Add(item);
            Console.WriteLine($"Student {item.Name} added to DB");
        }

        public void Remove(Student item)
        {
            students.Remove(item);
            Console.WriteLine($"Student {item.Name} Removed from DB");
        }

        public Student SearchById(int i)
        {
            return students.FirstOrDefault(s => s.Id == i);
        }

        //public void Update(Student item)
        //{
        //    int index = students.BinarySearch(item);
        //}
    }
    class Program
        {
        static void Main(string[] args)
        {
            StudentRepository r = new StudentRepository();
            r.Add(new Student(1, "Cristian", 810, "UTM"));
            r.Add(new Student(2, "Angela", 820, "UTM"));
            r.Add(new Student(3, "Oleg", 0, "USM"));
            Console.WriteLine(r.SearchById(2).Name);
            var students = r.List;
            foreach (var s in students)
                Console.WriteLine(s.Name);
            r.Remove(r.SearchById(1));
            Console.ReadKey();

        }
    }
}
