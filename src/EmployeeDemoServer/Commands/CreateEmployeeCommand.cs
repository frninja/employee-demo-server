using System;

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

        public Employee Execute()
        {
            Employee employee = new Employee();
            // TODO: Persist to DB using DbContext.
            return employee;
        }


        private readonly string name;
        private readonly string email;
        private readonly DateTime birthDay;
        private readonly int salary;
    }
}
