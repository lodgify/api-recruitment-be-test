using ApiApplication.DTOs.IMDB;
using System.Net;
using System.Threading.Tasks;

namespace ApiApplication.WebClients
{
    /// <summary>
    /// Specifices the contract for a web api client to IMDB.
    /// </summary>
    public interface IIMDBWebApiClient
    {
        /// <summary>
        /// Gets a movie from IMDB API with the specified imdb id.
        /// </summary>
        /// <param name="imdbId"></param>
        /// <returns></returns>
        Task<IMDBMovieInfo> GetMovieInfoAsync(string imdbId);

        /// <summary>
        /// Gets the HttpStatusCode corresponding to the status of the IMDB API.
        /// </summary>
        /// <returns></returns>
        Task<HttpStatusCode> GetStatus();
    }
}
