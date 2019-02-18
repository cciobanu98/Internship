using System;
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
   
     public static class Logger
     {
          public static void LoginUser(User u)
          {
               Console.WriteLine($"User {u.Name} has been logged");
          }
          public static void DeloginUser(User u)
          {
               Console.WriteLine($"User {u.Name} has been deloged");
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
          //public  int? Id { private set; get; }
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
          public Administartor(string name) : base(name)
          {

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
          public Moderator(string name) : base(name)
          {

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
          public SimplyUser(string name) : base(name)
          {

          }
          public override void WriteMessage(string Message)
          {
               Console.ForegroundColor = ConsoleColor.Blue;
               base.WriteMessage(Message);
               Console.ResetColor();
          }
     }
     class Program
     {
          static void Main(string[] args)
          {
               List<User> lst = new List<User>();
               lst.Add(new Administartor("Cristian"));
               lst.Add(new Administartor("Angela"));
               lst.Add(new Moderator("Oleg"));
               lst.Add(new SimplyUser("Alexandru"));
               lst.Add(new SimplyUser("Marian"));
               foreach (User u in lst)
               {
                    Logger.LoginUser(u);
                    u.WriteMessage($"Message from: {u.GetType().Name} {u.Name} with Id: {u.Id}");
                    Logger.DeloginUser(u);
               }
               Console.WriteLine($"Number of user {User.NumberOfUser}");
               Console.WriteLine($"Date of create: {User.TimeOfCreate}");
               Console.ReadKey();
          }
     }
}
