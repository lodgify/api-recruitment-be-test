namespace Lodgify.Cinema.Domain.Contract
{
    public interface IImdbIdTranslatorService
    {
        string Get(int id);
        int Get(string id);
    }
}
