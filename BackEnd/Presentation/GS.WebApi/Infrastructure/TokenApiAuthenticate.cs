//using GS.Core.Infrastructure;
//using GS.Services.Authentication;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Web.Http;
//using System.Web.Http.Filters;

//namespace GS.WebApi.Infrastructure
//{
//    public class TokenApiAuthenticate : Attribute, IAuthenticationFilter
//    {
//        public bool AllowMultiple
//        {
//            get
//            {
//                return false;
//            }
//        }

//        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
//        {
//            try
//            {
//                // Initialize authentication service

//                HttpRequestMessage request = context.Request;
//                AuthenticationHeaderValue authorization = request.Headers.Authorization;
//                if (authorization == null)
//                {
//                    context.ErrorResult = new AuthenticationFailureResult("Missing autorization header", request);
//                    return;
//                }

//                if (authorization.Scheme != "Bearer")
//                {
//                    context.ErrorResult = new AuthenticationFailureResult("Invalid autorization scheme", request);
//                    return;
//                }

//                if (string.IsNullOrEmpty(authorization.Parameter))
//                {
//                    context.ErrorResult = new AuthenticationFailureResult("Missing Token", request);
//                    return;
//                }
//                var authService = EngineContext.Current.Resolve<IAuthenticationService>();
//                var userinfo = authService.ValidateToken(authorization.Parameter);
//                if (userinfo != null)
//                {
//                    context.Request.Headers.Add("AuthorizedId", userinfo.ID.ToString());
//                    context.Request.Headers.Add("AuthorizedGuid", userinfo.GUID.ToString());
//                    context.Request.Headers.Add("AuthorizedName", userinfo.TEN_DANG_NHAP);
//                    context.Request.Headers.Add("AuthorizedFullName", userinfo.TEN_DAY_DU);
//                }
//                else
//                {
//                    context.ErrorResult = new AuthenticationFailureResult("Invalid Token", request);
//                }
//            }
//            catch (Exception ex)
//            {
//                context.ErrorResult = new AuthenticationFailureResult("Exception: \n" + ex.Message, context.Request);
//            }
//        }

//        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
//        {
//            var challenge = new AuthenticationHeaderValue("Basic");
//            context.Result = new AddChallengeOnUnauthorizedResult(challenge, context.Result);

//            return Task.FromResult(0);
//        }
//    }

//    public class AuthenticationFailureResult : IHttpActionResult
//    {
//        public string ReasonPhrase { get; private set; }
//        public HttpRequestMessage Request { get; private set; }

//        public AuthenticationFailureResult(string reasonPhrase, HttpRequestMessage request)
//        {
//            ReasonPhrase = reasonPhrase;
//            Request = request;
//        }

//        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
//        {
//            return Task.FromResult(Execute());
//        }

//        private HttpResponseMessage Execute()
//        {
//            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
//            response.RequestMessage = Request;
//            response.ReasonPhrase = ReasonPhrase;

//            return response;
//        }
//    }

//    public class AddChallengeOnUnauthorizedResult : IHttpActionResult
//    {
//        public AddChallengeOnUnauthorizedResult(AuthenticationHeaderValue challenge, IHttpActionResult innerResult)
//        {
//            Challenge = challenge;
//            InnerResult = innerResult;
//        }

//        public AuthenticationHeaderValue Challenge { get; private set; }
//        public IHttpActionResult InnerResult { get; private set; }

//        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
//        {
//            HttpResponseMessage response = await InnerResult.ExecuteAsync(cancellationToken);

//            if (response.StatusCode == HttpStatusCode.Unauthorized)
//            {
//                // Only add on challenge per authentication scheme.
//                if (!response.Headers.WwwAuthenticate.Any((h) => h.Scheme == Challenge.Scheme))
//                {
//                    response.Headers.WwwAuthenticate.Add(Challenge);
//                }
//            }

//            return response;
//        }
//    }
//}
