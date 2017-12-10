using System;

using FluentValidation;
using FluentValidation.Attributes;

using Newtonsoft.Json;

namespace SimpleCode.EmployeeDemoServer.Dto
{
    [Validator(typeof(CreateEmployeeDtoValidator))]
    public class CreateEmployeeDto
    {
        [JsonRequired]
        public string Name { get; set; }

        [JsonRequired]
        public string Email { get; set; }

        [JsonRequired]
        public DateTime BirthDay { get; set; }

        [JsonRequired]
        public int? Salary { get; set; }
    }


    public class CreateEmployeeDtoValidator : AbstractValidator<CreateEmployeeDto>
    {
        public CreateEmployeeDtoValidator() {
            RuleFor(e => e.Name).NotEmpty();
            RuleFor(e => e.Email).NotEmpty().EmailAddress();
            RuleFor(e => e.BirthDay).NotEmpty();
            RuleFor(e => e.Salary).NotEmpty().GreaterThan(0);
        }
    }
}
