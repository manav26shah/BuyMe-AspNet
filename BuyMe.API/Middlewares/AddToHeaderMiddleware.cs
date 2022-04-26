using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyMe.API.Middlewares
{
    public class AddToHeaderMiddleware
    {
        private RequestDelegate _next;

        public AddToHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.Headers.Add("custom-header", "3232");

            await _next(context);
        }
    }

    public static class AddToHeaderMiddlewareExtension
    {
        public static IApplicationBuilder AddCustomHeader(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AddToHeaderMiddleware>();
        }
    }
}
