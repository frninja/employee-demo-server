using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace SimpleCode.EmployeeDemoServer.WebApiExtensions.Results
{
    public class AddChallengeOnUnauthorizedResult : IHttpActionResult
    {
        public AuthenticationHeaderValue Challenge { get; }
        public IHttpActionResult WrappedResult { get; }

        public AddChallengeOnUnauthorizedResult(AuthenticationHeaderValue challenge, IHttpActionResult wrappedResult)
        {
            Challenge = challenge;
            WrappedResult = wrappedResult;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await WrappedResult.ExecuteAsync(cancellationToken).ConfigureAwait(false);
            if (response.StatusCode != HttpStatusCode.Unauthorized)
                return response;

            if (!response.Headers.WwwAuthenticate.Any(h => h.Scheme == Challenge.Scheme))
                return response;

            response.Headers.WwwAuthenticate.Add(Challenge);

            return response;
        }
    }
}
