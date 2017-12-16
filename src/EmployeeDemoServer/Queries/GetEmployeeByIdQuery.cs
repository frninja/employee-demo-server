using System;
using System.Threading.Tasks;

using SimpleCode.EmployeeDemoServer.Db;
using SimpleCode.EmployeeDemoServer.Exceptions;
using SimpleCode.EmployeeDemoServer.Models;

namespace SimpleCode.EmployeeDemoServer.Queries
{
    public class GetEmployeeByIdQuery : Query<Employee>
    {
        public Guid Id { get; }

        public GetEmployeeByIdQuery(Guid id)
        {
            Id = id;
        }

        public override async Task<Employee> Execute()
        {
            using (EmployeeContext context = new EmployeeContext())
            {
                Employee employee = await context.Employees.FindAsync(Id).ConfigureAwait(false);
                if (employee == null)
                    throw new ObjectNotFoundException();

                return employee;
            }
        }
    }
}
