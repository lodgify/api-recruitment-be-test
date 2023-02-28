using Lodgify.Cinema.Domain.Contract;

namespace Lodgify.Cinema.DomainService.Imdb
{
    public class ImdbStatus : IImdbStatus
    {
        public ImdbStatus(bool isUp)
        {
            IsUp = isUp;
        }

        public bool IsUp { get; private set; }

        public void SetStatus(bool newStatus)
        {
            IsUp = newStatus;
        }
    }
}