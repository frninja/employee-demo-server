using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SimpleCode.EmployeeDemoServer.WebApiExtensions.Filters
{
    public class JsonApiAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            var contentType = actionContext.Request.Content.Headers.ContentType;
            if (!string.Equals(contentType.MediaType, "application/json", System.StringComparison.InvariantCultureIgnoreCase))
            {
                actionContext.Response =
                    actionContext.Request.CreateErrorResponse(HttpStatusCode.UnsupportedMediaType, "API supports JSON only.");
            }
        }
    }
}
