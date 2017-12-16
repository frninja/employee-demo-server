using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

using SimpleCode.EmployeeDemoServer.Commands;
using SimpleCode.EmployeeDemoServer.Dto;
using SimpleCode.EmployeeDemoServer.Exceptions;
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
            return Ok(pagedEmployees);
        }

        [HttpGet]
        [Route("{id}", Name="GetEmployeeById")]
        public async Task<IHttpActionResult> GetById(Guid id)
        {
            GetEmployeeByIdQuery query = new GetEmployeeByIdQuery(id);
            try
            {
                Employee employee = await query.Execute().ConfigureAwait(false);
                // TODO: Use AutoMapper to return ViewDTO.
                return Ok(employee);
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
        // TODO: Pass UpdateDto argument.
        public async Task<IHttpActionResult> Update(Guid id)
        {
            // TODO: Validate input DTO.
            // TODO: Update employee using command object.
            Employee employee = null;
            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            // TODO: Remove employee using command object.
            return Ok();
        }
    }
}
