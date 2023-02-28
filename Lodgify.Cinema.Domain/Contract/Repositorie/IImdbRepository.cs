using System.Threading;
using System.Threading.Tasks;
using static Lodgify.Cinema.Domain.Dto.ImdbRepository;

namespace Lodgify.Cinema.Domain.Contract.Repositorie
{
    public interface IImdbRepository
    {
        Task<MovieResponse> GetMovieById(int id, CancellationToken cancellationToken);
    }
}