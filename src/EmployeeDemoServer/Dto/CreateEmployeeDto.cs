using System;

using FluentValidation;
using FluentValidation.Attributes;

namespace SimpleCode.EmployeeDemoServer.Dto
{
    [Validator(typeof(CreateEmployeeDtoValidator))]
    public class CreateEmployeeDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime BirthDay { get; set; }
        public int Salary { get; set; }
    }


    public class CreateEmployeeDtoValidator : AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeDtoValidator() {
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.Email).NotEmpty().EmailAddress();
            RuleFor(e => e.BirthDay).NotEmpty();
            RuleFor(e => e.Salary).GreaterThan(0);
        }
    }
}
