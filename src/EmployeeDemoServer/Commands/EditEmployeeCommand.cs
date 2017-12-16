using System;
using System.Threading.Tasks;

using SimpleCode.EmployeeDemoServer.Db;
using SimpleCode.EmployeeDemoServer.Exceptions;
using SimpleCode.EmployeeDemoServer.Models;

namespace SimpleCode.EmployeeDemoServer.Commands
{
    public class EditEmployeeCommand : Command<Employee>
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Email { get; }
        public DateTime BirthDay { get; }
        public int Salary { get; }


        public EditEmployeeCommand(Guid id, string name, string email, DateTime birthDay, int salary)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDay = birthDay;
            Salary = salary;
        }

        public override async Task<Employee> Execute()
        {
            using (EmployeeContext context = new EmployeeContext())
            {
                Employee employee = await context.Employees.FindAsync(Id).ConfigureAwait(false);
                if (employee == null)
                    throw new ObjectNotFoundException();

                employee.Name = Name;
                employee.Email = Email;
                employee.BirthDay = BirthDay;
                employee.Salary = Salary;

                context.Entry(employee).State = System.Data.Entity.EntityState.Modified;
                await context.SaveChangesAsync().ConfigureAwait(false);

                return employee;
            }
        }
    }
}
