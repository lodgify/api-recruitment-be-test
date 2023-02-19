namespace ApiApplication.Resources
{
    public interface IImdbPageStatus
    {
        ImdbStatus Status { get; set; }
    }

    public class ImdbPageStatus : IImdbPageStatus
    {
        public ImdbStatus Status { get; set ; }

        public ImdbPageStatus()
        {
            Status = new ImdbStatus { LastCall = "", Up = false };
        }
    }
}