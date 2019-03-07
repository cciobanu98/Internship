using System;
namespace IoC
{
    class LoginGoogle : ILogin
    {
        public void Login(User u)
        {
            Console.WriteLine($"Login {u.Name} from Google");
        }
    }
}