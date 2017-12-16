using System;
using System.Threading.Tasks;

namespace SimpleCode.EmployeeDemoServer.Commands
{
    public abstract class Command
    {
        public abstract Task Execute();
    }

    public abstract class Command<T>
    {
        public abstract Task<T> Execute();
    }
}
