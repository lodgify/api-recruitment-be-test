using ApiApplication.Models;

namespace ApiApplication.Services.Implementors
{
    public class ImdbApiStatusService : IImdbApiStatusService
    {
        public ImdbStatus imdbStatus { get; private set; } = new ImdbStatus();

    }
}
