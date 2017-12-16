using System;
using System.Threading.Tasks;

using SimpleCode.EmployeeDemoServer.Db;
using SimpleCode.EmployeeDemoServer.Models;

namespace SimpleCode.EmployeeDemoServer.Commands
{
    public class CreateEmployeeCommand
    {
        public string Name { get; }
        public string Email { get; }
        public DateTime BirthDay { get; }
        public int Salary { get; }

        public CreateEmployeeCommand(string name, string email, DateTime birthDay, int salary)
        {
            Name = name;
            Email = email;
            BirthDay = birthDay;
            Salary = salary;
        }

        public async Task<Employee> Execute()
        {
            Employee employee = new Employee(Guid.NewGuid(), Name, Email, BirthDay, Salary);

            using (EmployeeContext context = new EmployeeContext())
            {
                context.Employees.Add(employee);
                await context.SaveChangesAsync().ConfigureAwait(false);
            }

            return employee;
        }
    }
}
