using System;

using FluentValidation;
using FluentValidation.Attributes;

using SimpleCode.EmployeeDemoServer.Models;

namespace SimpleCode.EmployeeDemoServer.Dto
{
    [Validator(typeof(EditEmployeeDtoValidator))]
    public class EditEmployeeDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public int Salary { get; set; }
    }

    public class EditEmployeeDtoValidator : AbstractValidator<EditEmployeeDto>
    {
        public EditEmployeeDtoValidator()
        {
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.Email).NotEmpty().EmailAddress();
            RuleFor(e => e.BirthDay).NotEmpty();
            RuleFor(e => e.Salary).NotEmpty().GreaterThan(0);
        }
    }
}
