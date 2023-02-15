using CinemaApplication.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApiApplication.Controllers
{
    /// <summary>
    /// Exposes operations for current status
    /// </summary>
    public class StatusController : CinemaBaseApiController
    {
        /// <summary>
        /// Get the current IMDB status
        /// </summary>
        /// <returns>Imdb status info</returns>
        [HttpGet]
        public ActionResult<ImdbStatusDto> Get()
        {
            return Ok(new ImdbStatusDto()
            {
                Up = true,
                LastCall = DateTime.Now,
            });
        }
    }
}
