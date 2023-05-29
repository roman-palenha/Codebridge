namespace Codebridge.Middlewares.RateLimit
{
    public class RateLimitCounter
    {
        public static Dictionary<string, RateLimitCounter> RequestCounters { get; } = new();

        private int _totalRequests;
        private DateTime _lastRequestTime;

        public void IncrementRequests(TimeSpan timeSpan)
        {
            var currentTime = DateTime.UtcNow;
            if (_lastRequestTime == default || currentTime - _lastRequestTime > timeSpan)
            {
                _totalRequests = 1;
                _lastRequestTime = currentTime;
            }
            else
            {
                _totalRequests++;
            }
        }

        public bool IsRateLimited(int requestLimit)
        {
            return _totalRequests > requestLimit;
        }
    }
}
