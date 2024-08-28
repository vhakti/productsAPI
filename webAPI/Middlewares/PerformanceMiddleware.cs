using Microsoft.AspNetCore.Http.Features;
using static System.Net.WebRequestMethods;
using Util;
using System.Diagnostics;
using Microsoft.AspNetCore.Http.Extensions;

namespace webAPI.Middlewares
{
    public class PerformanceMiddleware
    {
        private readonly RequestDelegate _next;
        Logging _logger;
        public PerformanceMiddleware(RequestDelegate next)
        {
            _logger = new Logging("metrics.log");
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            string endpoint = context.Request.GetEncodedUrl();
             
            _logger.WriteInfo($"Init {endpoint}");
             Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
           
            await _next(context);

            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            _logger.WriteInfo($"Request time: {ts.TotalMinutes}.{ts.Seconds}.{ts.Milliseconds}");
        }
    }
}
