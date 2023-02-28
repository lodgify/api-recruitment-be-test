namespace Lodgify.Cinema.Domain.Contract
{
    public interface IImdbIdTranslatorService
    {
        string ImdbPrefixId { get; }
        string Get(int id);
        int Get(string id);
    }
}
