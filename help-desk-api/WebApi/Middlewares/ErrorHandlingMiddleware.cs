using Newtonsoft.Json;
using System.Net;
using Utils.Exceptions;

namespace WebApi.Middlewares;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ErrorHandlingMiddleware(
        RequestDelegate next,
        ILogger<ErrorHandlingMiddleware> logger,
        IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var errorResponse = exception switch
        {
            UnAuthorizedException e => BuildErrorResponse(e, HttpStatusCode.Unauthorized, e.ErrorDetail, e.Message, "Unauthorized"),
            SessionExpiredException e => BuildErrorResponse(e, (HttpStatusCode)440, e.ErrorDetail, e.Message, "Session Timeout"),
            ForbiddenException e => BuildErrorResponse(e, HttpStatusCode.Forbidden, e.ErrorDetail, e.Message, "Forbidden"),
            BadRequestException e => BuildErrorResponse(e, HttpStatusCode.BadRequest, e.ErrorDetail, e.Message, "Bad Request"),
            ResourceNotFoundException e => BuildErrorResponse(e, HttpStatusCode.NotFound, e.ErrorDetail, e.Message, "Not Found"),
            FileUploadException e => BuildErrorResponse(e, HttpStatusCode.InternalServerError, e.ErrorDetail, e.Message, "Upload Error"),
            RequestTimeoutException e => BuildErrorResponse(e, HttpStatusCode.RequestTimeout, e.ErrorDetail, e.Message, "Request Timeout"),

            Exception e => BuildErrorResponse(
                                                e,
                                                HttpStatusCode.InternalServerError,
                                                new ErrorDetail(_env.IsProduction() || _env.IsEnvironment("Test") ? "Something went wrong!" : e.Message),
                                                "Internal Error")
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = errorResponse.StatusCode;

        var result = JsonConvert.SerializeObject(errorResponse);
        await context.Response.WriteAsync(result);
    }

    private ErrorResponse BuildErrorResponse(Exception ex, HttpStatusCode statusCode, ErrorDetail errorDetail, string? message = null, string? title = null)
    {
        _logger.LogError(ex, "REST ERROR");

        return new ErrorResponse
        {
            StatusCode = (int)statusCode,
            Message = string.IsNullOrWhiteSpace(message) ? errorDetail?.Message : message,
            Title = string.IsNullOrWhiteSpace(title) ? errorDetail?.Title : title,
            Errors = errorDetail?.Errors
        };
    }
}
