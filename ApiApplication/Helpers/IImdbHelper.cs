using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Helpers
{
    public interface IImdbHelper
    {
        Task<Dictionary<string, object>> GetMovieInformationByIdAsync(string imdbId);

        Task<Dictionary<string, object>> GetMoviePostersByIdAsync(string imdbId);
    }
}
