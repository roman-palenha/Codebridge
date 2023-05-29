namespace Codebridge.Middlewares.RateLimit
{
    public class RateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly int _requestLimit;
        private readonly TimeSpan _timeSpan;

        public RateLimitMiddleware(RequestDelegate next, int requestLimit, TimeSpan timeSpan)
        {
            _next = next;
            _requestLimit = requestLimit;
            _timeSpan = timeSpan;
        }

        public async Task Invoke(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? string.Empty;

            if (IsRateLimited(ipAddress))
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Too Many Requests");
                return;
            }

            await _next(context);
        }

        private bool IsRateLimited(string ipAddress)
        {
            if(ipAddress == string.Empty)
                return false;

            if (!RateLimitCounter.RequestCounters.TryGetValue(ipAddress, out var counter))
            {
                counter = new RateLimitCounter();
                RateLimitCounter.RequestCounters[ipAddress] = counter;
            }

            counter.IncrementRequests(_timeSpan);
            return counter.IsRateLimited(_requestLimit);
        }
    }
}
