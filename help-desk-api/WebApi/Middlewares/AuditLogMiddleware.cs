using Amazon.SimpleEmailV2.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities.Audit;
using Services.Global; // your service layer namespace
using System;
using System.IO;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utils.LoginData;

namespace WebApi.Middlewares
{
    public class AuditLogMiddleware
    {
        private readonly RequestDelegate _next;

        public AuditLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var request = context.Request;
            request.EnableBuffering();

            string requestBody = string.Empty;
            if (request.ContentLength > 0 && request.ContentType != null && request.ContentType.Contains("application/json"))
            {
                using var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true);
                requestBody = await reader.ReadToEndAsync();
                request.Body.Position = 0;
            }

            // Capture original response stream
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            var scope = context.RequestServices.CreateScope();
            var auditService = scope.ServiceProvider.GetRequiredService<IAuditLogService>();
            var userInfo = scope.ServiceProvider.GetRequiredService<IUserInfos>();

            try
            {
                await _next(context);
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                string responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
                context.Response.Body.Seek(0, SeekOrigin.Begin);

                var userName = context.User.Identity?.IsAuthenticated == true
                    ? context.User.FindFirst(ClaimTypes.Name)?.Value ?? "System"
                    : "Anonymous";

                var auditLog = new QMSAuditLogModel
                {
                    FkUserId = userInfo.GetCurrentUserId().ToString(),
                    Method = request.Method,
                    RequestBody = requestBody,
                    ResponseBody = responseBodyText,
                    Path = request.Path,
                    LogAt = DateTime.UtcNow
                };
                await auditService.AddAsync(auditLog);

            }
            finally
            {
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }
    }
}
