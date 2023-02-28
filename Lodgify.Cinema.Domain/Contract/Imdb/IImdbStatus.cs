namespace Lodgify.Cinema.Domain.Contract
{
    public interface IImdbStatus
    {
        bool IsUp { get; }
        void SetStatus(bool newStatus);
    }
}