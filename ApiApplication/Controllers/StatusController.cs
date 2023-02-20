using ApiApplication.Services;
using CinemaApplication.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    /// <summary>
    /// Exposes operations for current status
    /// </summary>
    public class StatusController : CinemaBaseApiController
    {
        private readonly ImdbStatusModel _imdbStatusModel;

        public StatusController(
            ImdbStatusModel imdbStatusModel)
        {
            _imdbStatusModel = imdbStatusModel;
        }

        /// <summary>
        /// Get the current IMDB status
        /// </summary>
        /// <returns>Imdb status info</returns>
        [HttpGet]
        public ActionResult<ImdbStatusDto> Get()
            => Ok(new ImdbStatusDto
            {
                Up = _imdbStatusModel.Up,
                LastCall = _imdbStatusModel.LastCall
            });
    }
}
