using CinemaApplication.DTOs;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace ApiApplication.Middlewares
{
    internal class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var errorMessage = JsonConvert.SerializeObject(new ErrorDto
                {
                    Message = "Something went wrong. Please try again later."
                });

                await context.Response.WriteAsync(errorMessage);
            }
        }
    }
}
