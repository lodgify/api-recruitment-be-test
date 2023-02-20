using CinemaApplication.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Extensions
{
    public static class ServiceDataResultExtensions
    {
        internal static ActionResult<TData> ToActionResult<TData>(this ServiceDataResult<TData> serviceData)
        {
            if (serviceData.IsError)
                return new OkObjectResult(serviceData.Data);


            return new ObjectResult(serviceData.Data);
        }
    }
}
