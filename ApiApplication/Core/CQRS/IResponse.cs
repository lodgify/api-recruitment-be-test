namespace ApiApplication.Core.CQRS
{
    public interface IResponse
    {
    }

    public interface IPaginatedResponse : IResponse
    {
        string next_page_url { get; set; }
    }
}