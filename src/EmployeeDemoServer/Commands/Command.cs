using System.Threading.Tasks;

namespace SimpleCode.EmployeeDemoServer.Commands
{
    public abstract class Command<T>
    {
        public abstract Task<T> Execute();
    }
}
