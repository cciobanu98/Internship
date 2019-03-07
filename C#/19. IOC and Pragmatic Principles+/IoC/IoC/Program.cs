using System;
using System.Collections.Generic;
namespace IoC
{
    interface ILogin
    {
        void Login(User u);
    }
    class User
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public ILogin Login { get; set; }
        public User(string name, int id, ILogin login)
        {
            Name = name;
            Id = id;
            Login = login;
        }
        public void LoginUserInApp()
        {
            Login.Login(this);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<User> db = new List<User>();
            db.Add(new User("Cristian", 1, new LoginGoogle()));
            db.Add(new User("Angela", 2, new LoginFacebook()));
            db.Add(new User("Oleg", 3, new LoginUser()));
            foreach (var u in db)
                u.LoginUserInApp();
            Console.ReadKey();
        }
    }
}