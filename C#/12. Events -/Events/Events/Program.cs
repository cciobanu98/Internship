using System;
using System.Collections;
using System.Windows;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassExemple
{
    public interface IImportant
    {
        void ImportantMessage(string message);
    }
    public interface IDeleteMessage
    {
        void DeleteMessage(string message, User u);
        void Advertisment(User u);
    }
    public interface ILogger<T>
    {
        event EventHandler<User> MessageToSent;
        void SendMessageOfLogin(T data);
    }
    public class Logger<T> : ILogger<T> where T: User
    {
        public event EventHandler<User> MessageToSent;

        public  void LoginUser(User u)
        {
            Console.WriteLine($"User {u.Name} has been logged");
            MessageToSent?.Invoke(this, u);
        }
        public void DeloginUser(User u)
        {
            Console.WriteLine($"User {u.Name} has been deloged");
        }

        public void SendMessageOfLogin(T data)
        {
            var handler = MessageToSent;
            handler?.Invoke(this, data);
        }

    }
    public abstract class User
    {
        private static int numberOfUser = 1;
        public static int NumberOfUser
        {
            get
            {
                return numberOfUser - 1;
            }
            private set
            {
                numberOfUser = 1;
            }
        }
        public static DateTime TimeOfCreate { get; private set; }
        static User()
        {
            TimeOfCreate = DateTime.Now;
        }
        public readonly int? Id;
        public string Name { get; set; }
        public string Password { get; set; }
        public virtual void WriteMessage(string Message)
        {
            Console.WriteLine(Message);
        }
        public User()
        {
            Name = "";
            Id = null;
        }
        public virtual void  UserLogin(object sender, User s)
        {
            Console.WriteLine($"User {s.Name} has been logged");
        }
        public User(string name, ILogger<User> logger)
        {
            Name = name;
            Id = numberOfUser;
            numberOfUser++;
        }
        public User(string name)
        {
            Name = name;
            Id = numberOfUser;
            numberOfUser++;
        }
    }
    public class Administartor : User, IImportant, IDeleteMessage
    {
        public Administartor() : base()
        {

        }
        public Administartor(string name, ILogger<User> logger) : base(name)
        {
            logger.MessageToSent += AdministratorLogin;
        }
        void AdministratorLogin(object sender, User u)
        {
            Console.WriteLine($"To Administrator {this.Name} User login {u.Name}");
        }
        public void Advertisment(User u)
        {
            Console.WriteLine($"Administrator {this.Name} delete message from user {u.Name}");
        }

        public void DeleteMessage(string message, User u)
        {
            Console.WriteLine($"Message {message} has been delete");
            Advertisment(u);
        }

        public void ImportantMessage(string message)
        {
            Console.WriteLine("".PadLeft(100));
            Console.WriteLine($"Important Message from Administratot {message}");
            Console.WriteLine("".PadLeft(100));
        }

        public override void WriteMessage(string Message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            base.WriteMessage(Message);
            Console.ResetColor();
        }
    }
    public class Moderator : User, IDeleteMessage
    {
        public Moderator() : base()
        {

        }
        void ModeratorLogin(object sender, User u)
        {
            Console.WriteLine($"To moderator {this.Name} User login {u.Name}");
        }
        public Moderator(string name, ILogger<User> logger) : base(name)
        {
            logger.MessageToSent += ModeratorLogin;
        }

        public void Advertisment(User u)
        {
            Console.WriteLine($"Moderator {this.Name} delete message from user {u.Name}");
        }

        public void DeleteMessage(string message, User u)
        {
            Console.WriteLine($"Message {message} has been delete");
            Advertisment(u);
        }

        public override void WriteMessage(string Message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            base.WriteMessage(Message);
            Console.ResetColor();
        }
    }
    public class SimplyUser : User
    {
        public SimplyUser() : base()
        {

        }
        void SimplyUserLogin(object sender, User u)
        {
            Console.WriteLine($"To Simply User {this.Name}  User Login {u.Name}");
        }
        public SimplyUser(string name, ILogger<User> logger) : base(name)
        {
            logger.MessageToSent += SimplyUserLogin;
        }
        public override void WriteMessage(string Message)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            base.WriteMessage(Message);
            Console.ResetColor();
        }
    }
    class MyList<T> : List<T> where T: User
    {
        public delegate void ListDelegate(string item);
        public event ListDelegate OnAdd;
        public event ListDelegate OnRemove;
        public void  Add(T item)
        {
            if (OnAdd != null && item is User)
                OnAdd(item.Name);
            base.Add(item);
        }
        public void Remove(T item)
        {
            OnRemove?.Invoke(item.Name);
            base.Add(item);
        }

    }
    class Program
    {
        static void OnRemove(string s)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"User {s} has been delete from list");
            Console.ResetColor();
        }
        static void Message(object sender, User u)
        {
            Console.WriteLine(u.Name);
        }
        static void Main(string[] args)
        {
            Logger<User> logger = new Logger<User>();
            MyList<User> lst = new MyList<User>();
            lst.OnAdd += delegate (string s)
            {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine($"User {s} has been add to list");
                Console.ResetColor();
            };
            MyList<User>.ListDelegate deleg = (string s) => Console.WriteLine($"Send message to Administartor {s} has benn add to DB");
            lst.OnAdd += deleg;
            lst.OnRemove += OnRemove;
            lst.Add(new Administartor("Cristian", logger));
            lst.Add(new Administartor("Angela",logger));
            lst.Add(new Moderator("Oleg", logger));
            lst.OnAdd -= deleg;
            lst.Add(new SimplyUser("Alexandru", logger));
            lst.Add(new SimplyUser("Marian", logger));
            foreach (User u in lst)
            {
                logger.LoginUser(u);
                //logger.SendMessageOfLogin(u);
                u.WriteMessage($"Message from: {u.GetType().Name} {u.Name} with Id: {u.Id}");
                //logger.DeloginUser(u);
            }
            lst.Remove(lst[0]);
            Console.WriteLine($"Number of user {User.NumberOfUser}");
            Console.WriteLine($"Date of create: {User.TimeOfCreate}");
            Console.ReadKey();
        }
    }
}
