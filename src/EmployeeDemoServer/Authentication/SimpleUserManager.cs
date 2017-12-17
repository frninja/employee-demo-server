using System.Collections.Generic;

namespace SimpleCode.EmployeeDemoServer.Authentication
{
    public class UserManager
    {
        public UserManager()
        {
            users = new List<User> { new User("admin", "admin") };
        }

        public User FindUser(string userName, string password)
        {
            return users.Find(user => user.UserName == userName && user.Password == password);
        }

        private readonly List<User> users;
    }
}
