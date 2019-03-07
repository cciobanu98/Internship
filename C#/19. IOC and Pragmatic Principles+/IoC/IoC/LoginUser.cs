using System;
namespace IoC
{
    class LoginUser : ILogin
    {
        public void Login(User u)
        {
            Console.WriteLine($"Login {u.Name} from DB");
        }
    }
}