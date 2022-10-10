using ApiApplication.ImdbService.Models;
using IMDbApiLib.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiApplication.ImdbService.Service
{
    public interface IImdbService
    {
        Task<Movie> FetchMovieInformation(string imdbId);
        Task<NewMovieData> FetchCommingSoon();
    }
}
