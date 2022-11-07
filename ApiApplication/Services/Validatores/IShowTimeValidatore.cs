using ApiApplication.DTO;
using ApiApplication.Models;

namespace ApiApplication.Services.Validatores {
    public interface IShowTimeValidatore {
        Result<ShowTime> ValidateAdd(ShowTime item);
        Result<ShowTime> ValidateUpdate(ShowTime item);
    }
}
