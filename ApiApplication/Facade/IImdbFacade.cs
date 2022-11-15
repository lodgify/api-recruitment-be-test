using ApiApplication.Database.Entities;
using ApiApplication.IMDb;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiApplication.Services.Facade
{
    public interface IImdbFacade
    {
        Task<MovieEntity> DiscoverMovie(CriteriaImdb criteria);
        Task<HttpResponseMessage> GetMovieHttpResponseMessage(CriteriaImdb criteria);
    }
}
