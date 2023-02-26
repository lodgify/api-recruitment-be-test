using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

public class TrackingTimeMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<TrackingTimeMiddleware> _logger;

    public TrackingTimeMiddleware(RequestDelegate next, ILogger<TrackingTimeMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        await _next(context);

        stopwatch.Stop();
        var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
        _logger.LogInformation("{0} {1} executed in {2} ms", context.Request.Method, context.Request.Path, elapsedMilliseconds);
    }
}
