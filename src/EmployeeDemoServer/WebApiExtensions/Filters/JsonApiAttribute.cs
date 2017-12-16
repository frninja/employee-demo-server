using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SimpleCode.EmployeeDemoServer.WebApiExtensions.Filters
{
    public class JsonApiAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
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
