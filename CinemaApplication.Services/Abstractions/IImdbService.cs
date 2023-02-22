using CinemaApplication.Core.Models.ServiceResults;
using CinemaApplication.DTOs;
using CinemaApplication.Services.Models;
using System.Threading.Tasks;

namespace CinemaApplication.Services.Abstractions
{
    public interface IImdbService
    {
        Task<ServiceDataResult<ImdbStatusDto>> GetImdbStatus();

        Task<ServiceDataResult<ImdbApiMovie>> GetMovieAsync(string imdbId);
    }
}
