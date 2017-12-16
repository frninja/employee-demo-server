using AutoMapper;

using SimpleCode.EmployeeDemoServer.Dto;
using SimpleCode.EmployeeDemoServer.Models;

namespace SimpleCode.EmployeeDemoServer.Mapping
{
    public static class AutoMapperConfiguration
    {
        public static MapperConfiguration Get()
        {
            return new MapperConfiguration(cfg => {
                cfg.CreateMap<Employee, EmployeeViewDto>()
                    .ForMember(e => e.Id, opt => opt.MapFrom(e => e.Id))
                    .ForMember(e => e.Name, opt => opt.MapFrom(e => e.Name))
                    .ForMember(e => e.Email, opt => opt.MapFrom(e => e.Email))
                    .ForMember(e => e.BirthDay, opt => opt.MapFrom(e => e.BirthDay))
                    .ForMember(e => e.Salary, opt => opt.MapFrom(e => e.Salary));
            });
        }
    }
}
