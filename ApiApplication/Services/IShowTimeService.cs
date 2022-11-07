using System.Collections.Generic;
using System.Threading.Tasks;

using ApiApplication.DTO;
using ApiApplication.DTO.Queries;
using ApiApplication.Models;

namespace ApiApplication.Services {
    public interface IShowTimeService {
        Task<Result<IEnumerable<ShowTime>>> GetAllAsync(ShowTimeQuery query);
        Task<Result<ShowTime>> AddAsync(ShowTime item);
        Task<Result<ShowTime>> UpdateAsync(ShowTime item);
        Task<Result> DeleteAsync(int id);
    }
}
