using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema.Core.Extensions
{
    public static class ExecutionTrackingMiddlewareExtensions
    {
        public static void ConfigureExecutionTrackingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExecutionTrackingMiddleware>();
        }
    }
}
