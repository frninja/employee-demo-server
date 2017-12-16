using System;
using System.Threading.Tasks;

using SimpleCode.EmployeeDemoServer.Db;
using SimpleCode.EmployeeDemoServer.Exceptions;
using SimpleCode.EmployeeDemoServer.Models;

namespace SimpleCode.EmployeeDemoServer.Commands
{
    public class RemoveEmployeeCommand : Command
    {
        public Guid Id { get; }

        public RemoveEmployeeCommand(Guid id)
        {
            Id = id;
        }

        public override async Task Execute()
        {
            using (EmployeeContext context = new EmployeeContext())
            {
                Employee employee = await context.Employees.FindAsync(Id).ConfigureAwait(false);
                if (employee == null)
                    return;

                context.Employees.Remove(employee);
                await context.SaveChangesAsync().ConfigureAwait(false);
            }
        }
    }
}
