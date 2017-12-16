using System;
using System.Threading.Tasks;
using System.Web.Http;

using AutoMapper;

using SimpleCode.EmployeeDemoServer.Commands;
using SimpleCode.EmployeeDemoServer.Dto;
using SimpleCode.EmployeeDemoServer.Exceptions;
using SimpleCode.EmployeeDemoServer.Mapping;
using SimpleCode.EmployeeDemoServer.Models;
using SimpleCode.EmployeeDemoServer.Queries;
using SimpleCode.EmployeeDemoServer.WebApiExtensions.Filters;

namespace SimpleCode.EmployeeDemoServer.Controllers
{
    //[Authorize]
    [JsonApi]
    [RoutePrefix("api/employees")]
    public class EmployeeController : ApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll(int pageSize = 10, int pageNumber = 1,
                                                    string orderBy = "name", bool descending = false)
        {
            GetAllEmployeesQuery query = new GetAllEmployeesQuery(pageSize, pageNumber, orderBy, descending);
            var pagedEmployees = await query.Execute().ConfigureAwait(false);
            return Ok(pagedEmployees.Select(mapper.Map<Employee, EmployeeViewDto>));
        }

        [HttpGet]
        [Route("{id}", Name="GetEmployeeById")]
        public async Task<IHttpActionResult> GetById(Guid id)
        {
            GetEmployeeByIdQuery query = new GetEmployeeByIdQuery(id);
            try
            {
                Employee employee = await query.Execute().ConfigureAwait(false);
                return Ok(mapper.Map<Employee, EmployeeViewDto>(employee));
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create(CreateEmployeeDto dto)
        {
            if (dto == null)
                return BadRequest("Empty request body");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CreateEmployeeCommand command = new CreateEmployeeCommand(dto.Name, dto.Email,
                                                                      dto.BirthDay, dto.Salary.Value);
            Employee employee = await command.Execute().ConfigureAwait(false);
            return CreatedAtRoute("GetEmployeeById", new { id = employee.Id }, employee);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IHttpActionResult> Update(Guid id, EditEmployeeDto dto)
        {
            if (dto == null)
                return BadRequest("Empty request body");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            EditEmployeeCommand command = new EditEmployeeCommand(id, dto.Name, dto.Email, dto.BirthDay, dto.Salary);
            try
            {
                Employee employee = await command.Execute().ConfigureAwait(false);
                return Ok(mapper.Map<Employee, EmployeeViewDto>(employee));
            }
            catch (ObjectNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            // TODO: Remove employee using command object.
            return Ok();
        }


        private IMapper mapper = AutoMapperConfiguration.Get().CreateMapper();
    }
}
