using CinemaApplication.Core.Models.ServiceResults;
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

        internal static ActionResult ToActionResult(this ServiceResult serviceData)
        {
            return serviceData.Status switch
            {
                ServiceResultType.Success => new OkResult(),
                ServiceResultType.Failed => new ObjectResult(serviceData.Error) { StatusCode = StatusCodes.Status500InternalServerError },
                ServiceResultType.NotFound => new NotFoundObjectResult(serviceData.Error),
                ServiceResultType.ValidationFailed => new BadRequestObjectResult(serviceData.Error),
                _ => new ObjectResult(serviceData.Error) { StatusCode = StatusCodes.Status500InternalServerError }
            };
        }
    }
}
