using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassExemple
{
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
     public class Administartor:User
     {
          public Administartor() : base()
          {

          }
          public Administartor(string name):base(name)
          {

          }
          public override void WriteMessage(string Message)
          {
               Console.ForegroundColor = ConsoleColor.Red;
               base.WriteMessage(Message);
               Console.ResetColor();
          }
     }
     public class Moderator : User
     {
          public Moderator():base()
          {

          }
          public Moderator(string name):base(name)
          {

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
          public SimplyUser():base()
          {

          }
          public SimplyUser(string name):base(name)
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
               List <User> lst = new List<User>();
               lst.Add(new Administartor("Cristian"));
               lst.Add(new Administartor("Angela"));
               lst.Add(new Moderator("Oleg"));
               lst.Add(new SimplyUser("Alexandru"));
               lst.Add(new SimplyUser("Marian"));
               foreach (User u in lst)
                    u.WriteMessage($"Message from: {u.GetType().Name} {u.Name} with Id: {u.Id}");
               Console.WriteLine($"Number of user {User.NumberOfUser}");
               Console.WriteLine($"Date of create: {User.TimeOfCreate}");
               Console.ReadKey();
          }
     }
}
