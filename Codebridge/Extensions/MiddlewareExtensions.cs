using Codebridge.Business.Validation;
using Codebridge.Configs;
using Codebridge.Middlewares.RateLimit;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Codebridge.Configs.Interfaces;

namespace Codebridge.Extensions
{
    public static class MiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = $"Internal Server Error. {contextFeature.Error.Message}"
                        }.ToString());
                    }
                });
            });
        }

        public static void ConfigureRateLimitMiddleware(this IApplicationBuilder app, IConfiguration configuration)
        {
            var rateLimitConfig = configuration.GetSection("RateLimitConfig").Get<RateLimitConfig>();
            app.UseMiddleware<RateLimitMiddleware>(rateLimitConfig.RequestLimit, TimeSpan.FromSeconds(rateLimitConfig.TimeSpanSeconds));
        }
    }
}
