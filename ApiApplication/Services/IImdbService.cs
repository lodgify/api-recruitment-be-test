using System.Threading.Tasks;

using ApiApplication.DTO;
using ApiApplication.Models;

namespace ApiApplication.Services {
    public interface IImdbService {
        Task<Result<ImdbMovie>> GetAsync(string id);
    }
}
