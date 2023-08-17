using Messenger.App.Helpers;
using Messenger.App.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Messenger.App.Authorization
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDistributedCache _cache;
        private static string tokenRedisKey = "tokens";

        public JwtMiddleware(RequestDelegate next, IDistributedCache cache)
        {
            _next = next;
            _cache = cache;
        }

        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.GetAuthToken();

            var blackListedTokens = await _cache.GetRecordAsync<List<string>>(tokenRedisKey);

            if(blackListedTokens is not null && blackListedTokens.Contains(token))
            {
                await HandleExceptionAsync(context, new UnauthorizedAccessException());
            }

            var userId = jwtUtils.ValidateToken(token);
            if (userId is not null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = userService.GetById(userId);
            }

            await _next(context);
        }

        private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var statusCode = StatusCodes.Status401Unauthorized;
            var response = new
            {
                title = GetTitle(exception),
                status = statusCode,
                detail = exception.Message,
            };
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static string GetTitle(Exception exception) =>
            exception switch
            {
                ApplicationException applicationException => applicationException.Message,
                _ => "Server Error"
            };
    }
}
