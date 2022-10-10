using ApiApplication.Models;

namespace ApiApplication.Services
{
    public interface IImdbApiStatusService
    {
        ImdbStatus imdbStatus { get; }
    }
}
