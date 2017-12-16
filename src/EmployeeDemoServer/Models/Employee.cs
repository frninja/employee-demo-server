using System;
using System.ComponentModel.DataAnnotations;

namespace SimpleCode.EmployeeDemoServer.Models
{
    public class Employee
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        // TODO: Validate email.
        [Required]
        public string Email { get; set; }

        // TODO: Validate date.
        [Required]
        public DateTime BirthDay { get; set; }

        // TODO: Validate > 0.
        [Required]
        public int Salary { get; set; }

        public Employee(Guid id, string name, string email, DateTime birthDay, int salary)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDay = birthDay;
            Salary = salary;
        }

        // Private constructor for ORM.
        private Employee() {}
    }
}
