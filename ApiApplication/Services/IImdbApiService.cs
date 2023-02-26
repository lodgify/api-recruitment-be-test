using ApiApplication.Database.Entities;
using System.Net;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    public interface IImdbApiService
    {
        public Task<TitleImdbEntity> GetMovieAsync(string imdbId);

        public Task<HttpStatusCode> GetImdbStatusAsync();
    }
}
