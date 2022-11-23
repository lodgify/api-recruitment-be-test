using ApiApplication.Models.Movies;
using IMDbApiLib;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace ApiApplication.Services.IMDB
{
    public class ImdbService : IImdbService
    {
        private readonly ILogger<ImdbService> _logger;
        private string _apiKey = "k_fqyklwwq";

        public ImdbService(ILogger<ImdbService> logger)
        {
            _logger = logger;
        }

        public async Task<bool> CheckApiStatus()
        {
            var apiLib = new ApiLib(_apiKey);

            try
            {
                var data = await apiLib.InTheatersAsync();

                return data.Items != null;

            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message, exception);

                return false;
            }

        }

        public async Task<MovieModel> GetByImdbId(string imdbId)
        {

            try
            {
                var apiLib = new ApiLib(_apiKey);

                var data = await apiLib.TitleAsync(imdbId);

                return new MovieModel
                {
                    ImdbId = imdbId,
                    Title = data.Title,
                    Stars = data.Stars,
                    ReleaseDate = DateTime.Parse(data.ReleaseDate)
                };
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message, exception);

                return new MovieModel();
            }
        }
    }
}
