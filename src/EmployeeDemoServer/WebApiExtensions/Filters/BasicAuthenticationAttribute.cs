using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Filters;

using SimpleCode.EmployeeDemoServer.WebApiExtensions.Results;
using System.Security.Principal;

namespace SimpleCode.EmployeeDemoServer.WebApiExtensions.Filters
{
    public class BasicAuthenticationAttribute : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple => false;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken) {
            HttpRequestMessage request = context.Request;
            AuthenticationHeaderValue authentication = request.Headers.Authorization;

            if (authentication == null)
                return;

            if (!authentication.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase))
                return;

            if (string.IsNullOrEmpty(authentication.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("Missing credentials.", request);
                return;
            }

            Tuple<string, string> userNameAndPassword = ExtractUserNameAndPassword(authentication.Parameter);
            if (userNameAndPassword == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid credentials.", request);
                return;
            }

            IPrincipal principal = await DoAuthenticateAsync(userNameAndPassword.Item1,
                                                             userNameAndPassword.Item2,
                                                             cancellationToken)
                                            .ConfigureAwait(false);

            if (principal == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("Invalid user name or password.", request);
                return;
            }

            context.Principal = principal;
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken) {
            throw new NotImplementedException();
        }


        private Tuple<string, string> ExtractUserNameAndPassword(string authenticationParameter)
        {
            byte[] credentialsBytes;
            try
            {
                credentialsBytes = Convert.FromBase64String(authenticationParameter);
            }
            catch (FormatException)
            {
                return null;
            }

            Encoding encoding = (Encoding)Encoding.ASCII.Clone();
            encoding.DecoderFallback = DecoderFallback.ExceptionFallback;

            string decodedCredentials;
            try
            {
                decodedCredentials = encoding.GetString(credentialsBytes);
            }
            catch (DecoderFallbackException)
            {
                return null;
            }

            if (string.IsNullOrEmpty(decodedCredentials))
                return null;

            int colonIndex = decodedCredentials.IndexOf(':');
            if (colonIndex == -1)
                return null;

            string userName = decodedCredentials.Substring(0, colonIndex);
            string password = decodedCredentials.Substring(colonIndex + 1);
            return new Tuple<string, string>(userName, password);
        }

        private async Task<IPrincipal> DoAuthenticateAsync(string userName, string password, CancellationToken cancellationToken)
        {
            if (!(userName == "admin" && password == "admin"))
                return null;

            GenericIdentity identity = new GenericIdentity("admin", "Basic");
            GenericPrincipal principal = new GenericPrincipal(identity, null);

            return principal;
        }
    }
}