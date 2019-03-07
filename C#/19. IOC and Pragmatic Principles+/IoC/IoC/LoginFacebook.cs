using System;
namespace IoC
{
    class LoginFacebook : ILogin
    {
        public void Login(User u)
        {
            Console.WriteLine($"Login {u.Name} from Facebook");
        }
    }
}