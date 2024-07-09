using System.Net.Http;
using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using BP.Business.Common.Exceptions;
using BP.Api.Common.Constants;

namespace BP.Api.Common.Middleware
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotFoundException notFoundException)
            {
                var code = HttpStatusCode.NotFound;
                string message = notFoundException.Message;
                _logger.LogError(message);
                await HandleExceptionAsync(context, code, message);
            }
            catch (InvalidCredentialsException wrongPasswordExcepton)
            {
                var code = HttpStatusCode.Unauthorized;
                string message = wrongPasswordExcepton.Message;
                _logger.LogError(message);
                await HandleExceptionAsync(context, code, message);
            }
            catch (ServerConflictException serverConflictException)
            {
                var code = HttpStatusCode.Conflict;
                string message = serverConflictException.Message;
                _logger.LogError(message);
                await HandleExceptionAsync(context, code, message);
            }
            catch (AccessDeniedException accessDeniedException)
            {
                var code = HttpStatusCode.Forbidden;
                string message = accessDeniedException.Message;
                await HandleExceptionAsync(context, code, message);
            }
            catch (Exception exception)
            {
                var code = HttpStatusCode.InternalServerError;
                var message = exception.Message;
                _logger.LogError(message);
                await HandleExceptionAsync(context, code, message);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, HttpStatusCode code, string message)
        {
            var result = JsonSerializer.Serialize(message);
            var httpResponse = context.Response;
            httpResponse.ContentType = ExceptionHandlingValues.ResponseContentType;
            httpResponse.StatusCode = (int)code;
            await httpResponse.WriteAsync(result);
        }
    }
}
