using System;

namespace SimpleCode.EmployeeDemoServer.Dto
{
    public class EmployeeViewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public int Salary { get; set; }
    }
}
