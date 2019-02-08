using System;

namespace OOPAdvanced
{
    interface ILogin
    {
        void Login();
    }
    interface IRegister
    {
        void Register();
    }
    interface IModerator
    {
        void Advertisment();
    }
    interface IMessage
    {
        void WriteMessage();
    }
    class User:ILogin, IRegister, IMessage
    {
        public int id { set; get; }
        public string name { set; get; }
        public string password { get; set; }
        public void Login()
        {
            Decrypt();
            Console.WriteLine("User Login");
        }
        public void Register()
        {
            Encrypt();
            Console.WriteLine("User Register");
        }
        private void Encrypt() => Console.WriteLine("Encrypt Password");
        private void Decrypt() => Console.WriteLine("Decrypt password");
        public virtual void WriteMessage() => Console.WriteLine("Write Message");
    }
    class Admin:User
    {
        public void SetPrivileges() => Console.WriteLine("Set privileges");
        public override void WriteMessage()
        {
            Console.WriteLine("Message from Admin")
        }
    }
    class Moderator:User, IModerator
    {
        public void DeleteMessage() => Console.WriteLine("Delete Message");
        public void Advertisment() => Console.WriteLine("Advertisment");
    }
    class SimplyUser:User
    {

    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
