using ApiApplication.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ApiApplication.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestTimeCalculatorMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<RequestTimeCalculatorMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }


        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {

                await _next(httpContext);
            }

            catch (InternalServerException exception)
            {
                _logger.LogError($"{(int)exception.StatusCode} {exception.StatusCode}: {exception.Message}", exception);

                httpContext.Response.StatusCode = (int)exception.StatusCode;

                await httpContext.Response.WriteAsync($"{(int)exception.StatusCode} {exception.StatusCode}: {exception.Message}");
            }

            catch (Exception exception)
            {

                _logger.LogError(exception.Message, exception);


            }


        }


    }
}
