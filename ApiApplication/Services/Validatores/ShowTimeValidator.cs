using ApiApplication.Database;
using ApiApplication.DTO;
using ApiApplication.Models;

namespace ApiApplication.Services.Validatores {
    public class ShowTimeValidator : IShowTimeValidatore {
        private readonly IShowtimesRepository _showtimesRepository;

        public ShowTimeValidator(IShowtimesRepository showtimesRepository) {
            _showtimesRepository = showtimesRepository;
        }

        public Result<ShowTime> ValidateAdd(ShowTime item) {
            if (string.IsNullOrWhiteSpace(item.Movie?.ImdbId))
                return new Result<ShowTime>(ResultCode.BadRequest, "Imdb Id cann't be empty");

            if (!_showtimesRepository.AuditoriumExists(item.AuditoriumId))
                return new Result<ShowTime>(ResultCode.BadRequest, "Auditorium not exists.");

            return new Result<ShowTime>(ResultCode.Ok);
        }

        public Result<ShowTime> ValidateUpdate(ShowTime item) {
            if (item.Movie == null)
                return new Result<ShowTime>(ResultCode.NoContent);
            if (item.Id == 0)
                return new Result<ShowTime>(ResultCode.BadRequest, "Please set id");

            return ValidateAdd(item);
        }
    }
}
