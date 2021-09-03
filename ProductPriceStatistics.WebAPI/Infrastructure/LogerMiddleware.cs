using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ProductStoreMicroservice.Infrastructure
{
    public class LogerMiddleware
    {
        private RequestDelegate _nextDelegate;
        private readonly ILogger<LogerMiddleware> _logger;

        public LogerMiddleware(RequestDelegate next, ILogger<LogerMiddleware> logger) 
        {
            _nextDelegate = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext) 
        {
            _logger.LogInformation("{Host} request {Path}", httpContext.Request.Host, httpContext.Request.Path);
            await _nextDelegate.Invoke(httpContext);
        }
    }
}
