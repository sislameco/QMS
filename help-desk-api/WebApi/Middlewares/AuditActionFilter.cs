using Microsoft.AspNetCore.Mvc.Filters;
using Models.Entities.Audit;
using Services.Global;
using System.Text.Json;
using Utils.LoginData;

namespace WebApi.Middlewares
{
    public class AuditActionFilter : IAsyncActionFilter
    {
        private readonly IAuditLogService _auditService;
        private readonly IUserInfos _userInfo;

        public AuditActionFilter(IAuditLogService auditService, IUserInfos userInfo)
        {
            _auditService = auditService;
            _userInfo = userInfo;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var controller = context.Controller.GetType().Name;
            var action = context.ActionDescriptor.DisplayName;

            // ✅ You can now access the actual DTO object
            if (context.ActionArguments.TryGetValue("input", out var inputDto) && action == "UpdateBasicDetails")
            {
                var dtoJson = JsonSerializer.Serialize(inputDto);

                await _auditService.AddAsync(new QMSAuditLogModel
                {
                    FkUserId = _userInfo.GetCurrentUserId().ToString(),
                    Method = context.HttpContext.Request.Method,
                    Path = context.HttpContext.Request.Path,
                    RequestBody = dtoJson,
                    LogAt = DateTime.UtcNow
                });
            }

            await next();
        }
    }
}