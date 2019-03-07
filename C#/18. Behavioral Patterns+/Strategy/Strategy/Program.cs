using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strategy
{
     abstract class  LoginClass
     {
          public void Login()
          {
               SearchUser();
               CheckPassword();
               HelloMessage();

          }
          public abstract void SearchUser();
          public virtual void CheckPassword()
          {
               Console.WriteLine("Check password");
          }
          public void HelloMessage()
          {
               Console.ForegroundColor = ConsoleColor.Red;
               Console.WriteLine("Hello");
               Console.ResetColor();
          }
     }
     class LoginFacebook : LoginClass
     {
          public override void SearchUser()
          {
               Console.WriteLine("Search User in FB");
          }
          public override void CheckPassword()
          {
               Console.WriteLine("Get Hash from FB");
          }
     }
     class LoginUsername:LoginClass
     {
          public override void SearchUser()
          {
               Console.WriteLine("Search User in Local Db");
          }
     }
     class LoginGoogle:LoginClass
     {
          public override void SearchUser()
          {
               Console.WriteLine("Search User in Google");
          }
          public override void CheckPassword()
          {
               Console.WriteLine("Get hash from Google");
          }
     }
     class User
     {
          private LoginClass loginMethod;
          public string UserName { get; set; }
          public User(string userName, LoginClass login)
          {
               loginMethod = login;
               UserName = userName;
          }
          public void Login()
          {
               Console.WriteLine($"User {UserName} ");
               loginMethod.Login();
               Console.WriteLine();
          }
     }
     class Program
     {
          static void Main(string[] args)
          {
               User u1 = new User("Cristian", new LoginFacebook());
               User u2 = new User("Angela", new LoginUsername());
               User u3 = new User("Oleg", new LoginGoogle());
               u1.Login();
               u2.Login();
               u3.Login();
               Console.ReadKey();
          }
     }
}
