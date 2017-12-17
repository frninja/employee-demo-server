namespace SimpleCode.EmployeeDemoServer.Authentication
{
    public class User
    {
        public string UserName { get; }
        public string Password { get; }

        public User(string userName, string password) {
            UserName = userName;
            Password = password;
        }
    }
}
