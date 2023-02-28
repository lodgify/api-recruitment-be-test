using System.Threading.Tasks;

namespace Lodgify.Cinema.Domain.Contract
{
    public interface IImdbStatusService
    {
        Task<IImdbStatus> IsUp();
    }
}