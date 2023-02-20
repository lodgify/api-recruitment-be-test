using CinemaApplication.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Extensions
{
    public static class ServiceDataResultExtensions
    {
        internal static ActionResult<TData> ToActionResult<TData>(this ServiceDataResult<TData> serviceData)
        {
            return serviceData.Status switch
            {
                ServiceResultType.Success => new OkObjectResult(serviceData.Data),
                ServiceResultType.Failed => new ObjectResult(serviceData.Data) { StatusCode = StatusCodes.Status500InternalServerError },
                ServiceResultType.Created => new ObjectResult(serviceData.Data) { StatusCode = StatusCodes.Status201Created },
                ServiceResultType.NotFound => new NotFoundObjectResult(serviceData.Error),
                ServiceResultType.ValidationFailed => new BadRequestObjectResult(serviceData.Error),
                _ => new ObjectResult(serviceData.Data) { StatusCode = StatusCodes.Status500InternalServerError }
            };
        }
    }
}
