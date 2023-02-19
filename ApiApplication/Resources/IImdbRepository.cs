using System.Threading.Tasks;

namespace ApiApplication.Resources
{
    public interface IImdbRepository
    {
        Task<ImdbTitleResponse> GetTitle(string imdbId);
    }
}