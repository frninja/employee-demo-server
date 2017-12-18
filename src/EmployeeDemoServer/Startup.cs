using Owin;
using Microsoft.Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(SimpleCode.EmployeeDemoServer.Startup))]
namespace SimpleCode.EmployeeDemoServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);

            app.UseWebApi(config);
        }
    }
}
