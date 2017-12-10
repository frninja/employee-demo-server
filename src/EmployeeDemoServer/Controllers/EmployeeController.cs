using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

using SimpleCode.EmployeeDemoServer.Models;

namespace SimpleCode.EmployeeDemoServer.Controllers
{
    //[Authorize]
    [RoutePrefix("api/employees")]
    public class EmployeeController : ApiController
    {
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAll()
        {
            // TODO: Get all employee using query object.
            IEnumerable<Employee> employees = new Employee[0];
            return Ok();
        }

        [HttpGet]
        [Route("{id}", Name="GetEmployeeById")]
        public async Task<IHttpActionResult> GetById(Guid id)
        {
            // TODO: Get employee using query object.
            Employee employee = null;
            return Ok(employee);
        }

        [HttpPost]
        [Route("")]
        // TODO: Pass CreateDto argument.
        public async Task<IHttpActionResult> Create()
        {
            // TODO: Validate input DTO.
            // TODO: Create employee using command object.
            Employee employee = null;
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
