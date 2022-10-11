using ApiApplication.ImdbService.Models;
using IMDbApiLib;
using IMDbApiLib.Models;
using System;
using System.Threading.Tasks;

namespace ApiApplication.ImdbService.Service.Implementors
{
    public class ImdbService : IImdbService
    {
        private ApiLib imdbApiLib;
        public ImdbService(ApiLib apiLib) 
        {
            imdbApiLib = apiLib;
        }

        public async Task<Movie> FetchMovieInformation(string imdbId)
        {
            var result = await imdbApiLib.TitleAsync(imdbId);
            Movie movieData = new Movie() {
                ImdbId = imdbId,
                Title = result.Title,
                ReleaseDate = Convert.ToDateTime(result.ReleaseDate),
                Stars = result.Stars,
            };
            return movieData;
        }

        public Task<NewMovieData> FetchCommingSoon()
        {
            return imdbApiLib.ComingSoonAsync();
        }

    }
}
