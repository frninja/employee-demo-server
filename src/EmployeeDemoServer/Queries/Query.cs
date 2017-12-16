using System.Threading.Tasks;

namespace SimpleCode.EmployeeDemoServer.Queries
{
    public abstract class Query<T>
    {
        public abstract Task<T> Execute();
    }
}
