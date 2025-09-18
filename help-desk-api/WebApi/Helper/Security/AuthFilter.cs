using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Models.AppSettings;
using Services.UserManagement;
using System.Security.Claims;
using System.Text;
using Utils.Exceptions;
using Utils.JWT;
using WebApi.Helper.Security;

namespace WebApi.Helper
{
    public class AuthFilter : ActionFilterAttribute, IAsyncActionFilter
    {
        private readonly IUserService _userService;

        public AuthFilter(IUserService userService)
        {
            _userService = userService;
        }
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var isAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (isAnonymous)
            {
                await next();
                return;
            }
            string authHeader = Convert.ToString(context.HttpContext.Request?.Headers["Authorization"]);
            string apiQueyKey = context.HttpContext.Request?.Query["accessKey"];

            if (string.IsNullOrEmpty(authHeader ?? apiQueyKey))
                throw new UnAuthorizedException("Authorization header is missing. Please include a valid Authorization header in your request.");

            if (authHeader !=null&& authHeader.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
                HandleBasicAuth(context, authHeader);
            else if (authHeader != null && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
                HandleBearerAuth(context, authHeader);
            else if (!string.IsNullOrEmpty( apiQueyKey))
                HandleApiKeyAuth(context, apiQueyKey);
            else
                throw new UnAuthorizedException("Invalid Authorization Header");

            await next();
        }

        private void HandleBearerAuth(ActionExecutingContext context, string authHeader)
        {
            var token = authHeader.Split(' ')[1];
            var parseToken = JwtTokenDecode.GetTokenDecode(token);

            if (parseToken == null)
                throw new UnAuthorizedException("Invalid Access Token");

            var queryParams = context.HttpContext.Request.Query;


            SaveUserSession(context.HttpContext, parseToken.FirstName, parseToken.UserId, "Bearer");

            //Checking Authorization in a particular end point and method
            //If end point method has CustomAuthorization attribute, we will check user has permission on that method
            if (context.ActionDescriptor.EndpointMetadata.OfType<CustomAuthorization>().Any())
                new CustomAuthorization().OnAuthorization(context, parseToken.UserId);
        }

        private void HandleBasicAuth(ActionExecutingContext context, string authHeader)
        {
            var token = authHeader.Substring("Basic ".Length).Trim();
            var credentials = DecodeBase64(token).Split(':', 2);

            if (credentials.Length != 2 || !IsValidUser(credentials[0], credentials[1]))
                throw new UnAuthorizedException("Invalid Access Credential");

            var systemUser = _userService.GetSystemUser();
            if (systemUser == null)
                throw new UnAuthorizedException("Failed to authenticate.");

            SaveUserSession(context.HttpContext, systemUser.FirstName, systemUser.Id.ToString(), "Basic");
        }

        private void HandleApiKeyAuth(ActionExecutingContext context, string apiKeyValue)
        {
            //if (apiKeyValue != AppSettings.ApiKeyAuthCrediantial.KeyValue)
            //    throw new UnAuthorizedException("Invalid Access Credential");

            var systemUser = _userService.GetSystemUser();

            if (systemUser == null)
                throw new UnAuthorizedException("Failed to authenticate.");

            SaveUserSession(context.HttpContext, systemUser.FirstName, systemUser.Id.ToString(), "Basic");
        }


        private bool IsValidUser(string accessKey, string accessSecret)
        {
            // Validate credentials. Replace with your actual validation logic.
            return accessKey == AppSettings.BasicAuthCredential.AccessKey && accessSecret == AppSettings.BasicAuthCredential.AccessSecret;
        }

        private string DecodeBase64(string base64String)
        {
            var byteArray = Convert.FromBase64String(base64String);
            return Encoding.UTF8.GetString(byteArray);
        }

        private void SaveUserSession(HttpContext context, string userName, string userId, string scheme)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim("userId", userId),
            };
            var identity = new ClaimsIdentity(claims, scheme);
            context.User = new ClaimsPrincipal(identity);
        }

    }
}
