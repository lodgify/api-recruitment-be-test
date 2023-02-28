using System.Threading;
using System.Threading.Tasks;
using static Lodgify.Cinema.Domain.Dto.ImdbRepository;

namespace Lodgify.Cinema.Domain.Contract.Repositorie
{
    public interface IImdbRepository
    {
        Task<MovieResponse> GetMovieByIdAsync(int id, CancellationToken cancellationToken);
        Task<bool> HealtCheckStatusAsync(CancellationToken cancellationToken);
    }
}