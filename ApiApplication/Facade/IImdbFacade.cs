using ApiApplication.Database.Entities;
using ApiApplication.Models.ViewModels;
using System.Net.Http;
using System.Threading.Tasks;

namespace ApiApplication.Facade
{
    public interface IImdbFacade
    {
        Task<MovieEntity> DiscoverMovie(CriteriaImdb criteria);
        Task<HttpResponseMessage> GetHttpResponseMessage(string baseUri, string searchCriteria);
    }
}
