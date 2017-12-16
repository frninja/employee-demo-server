using System;
using System.Threading.Tasks;

using SimpleCode.EmployeeDemoServer.Db;
using SimpleCode.EmployeeDemoServer.Models;

namespace SimpleCode.EmployeeDemoServer.Commands
{
    public class CreateEmployeeCommand
    {
        public CreateEmployeeCommand(string name, string email, DateTime birthDay, int salary)
        {
            this.name = name;
            this.email = email;
            this.birthDay = birthDay;
            this.salary = salary;
        }

        public async Task<Employee> Execute()
        {
            Employee employee = new Employee(Guid.NewGuid(), name, email, birthDay, salary);

            using (EmployeeContext context = new EmployeeContext())
            {
                context.Employees.Add(employee);
                await context.SaveChangesAsync().ConfigureAwait(false);
            }

            return employee;
        }


        private readonly string name;
        private readonly string email;
        private readonly DateTime birthDay;
        private readonly int salary;
    }
}
