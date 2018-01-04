using System.Web.Http;
using System.Threading.Tasks;

using SimpleCode.EmployeeDemoServer.WebApiExtensions.Filters;

namespace SimpleCode.EmployeeDemoServer.Controllers
{
    public class AuthenticationController : ApiController
    {
        [BasicAuthentication]
        [Authorize]
        [HttpPost]
        [Route("api/authenticate")]
        public async Task<IHttpActionResult> Authenticate() {
            return Ok();
        }
    }
}
