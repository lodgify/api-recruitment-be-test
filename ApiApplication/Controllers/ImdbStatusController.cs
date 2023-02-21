using ApiApplication.Services;
using CinemaApplication.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ApiApplication.Controllers
{
    /// <summary>
    /// Exposes operations managing Imdb status
    /// </summary>
    public class ImdbStatusController : CinemaBaseApiController
    {
        private readonly ImdbStatusModel _imdbStatusModel;

        /// <summary>
        /// Constructor
        /// </summary>
        public ImdbStatusController(
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
