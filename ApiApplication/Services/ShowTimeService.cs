using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.DTO;
using ApiApplication.DTO.Queries;
using ApiApplication.Models;
using ApiApplication.Services.Validatores;
using ApiApplication.Utils;

namespace ApiApplication.Services {
    public class ShowTimeService : IShowTimeService {
        private readonly IImdbService _imdbService;
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly IShowTimeValidatore _showTimeValidatore;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IObjectMapper _mapper;

        public ShowTimeService(IImdbService imdbService,
                               IShowtimesRepository showtimesRepository,
                               IShowTimeValidatore showTimeValidatore,
                               IUnitOfWork unitOfWork,
                               IObjectMapper mapper) {
            _imdbService = imdbService;
            _showtimesRepository = showtimesRepository;
            _showTimeValidatore = showTimeValidatore;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<ShowTime>> AddAsync(ShowTime item) {
            Result<ShowTime> validationResult = _showTimeValidatore.ValidateAdd(item);
            if (!validationResult.Success)
                return validationResult;

            Result getMovieInfoResult = await FillMovieInfo(item);
            if (!getMovieInfoResult.Success)
                return new Result<ShowTime>(getMovieInfoResult.Code, getMovieInfoResult.Message);

            ShowtimeEntity addedEntity = await _showtimesRepository.AddAsync(_mapper.Map<ShowtimeEntity>(item));
            await _unitOfWork.SaveChangesAsync();
            return new Result<ShowTime>(ResultCode.Ok) {
                Data = _mapper.Map<ShowTime>(addedEntity)
            };
        }

        public async Task<Result> DeleteAsync(int id) {
            ShowtimeEntity deletedItem = await _showtimesRepository.DeleteAsync(id);
            if (deletedItem == null)
                return new Result(ResultCode.NotFound);
            await _unitOfWork.SaveChangesAsync();
            return new Result(ResultCode.Ok);
        }

        public Task<Result<IEnumerable<ShowTime>>> GetAllAsync(ShowTimeQuery query) {
            IEnumerable<ShowtimeEntity> entities = null;
            if (query == null || (string.IsNullOrWhiteSpace(query.MovieTitle) && !query.StartDate.HasValue))
                entities = _showtimesRepository.GetCollection();
            else
                entities = _showtimesRepository.GetCollection(s => (!query.StartDate.HasValue || s.StartDate == query.StartDate)
                                                                && (string.IsNullOrWhiteSpace(query.MovieTitle) || string.Equals(query.MovieTitle, s.Movie.Title,
                                                                                                                                 StringComparison.OrdinalIgnoreCase)));
            return Task.FromResult(new Result<IEnumerable<ShowTime>>(ResultCode.Ok) {
                Data = _mapper.Map<IEnumerable<ShowTime>>(entities)
            });
        }

        public async Task<Result<ShowTime>> UpdateAsync(ShowTime item) {
            

            Result<ShowTime> validationResult = _showTimeValidatore.ValidateUpdate(item);
            if (!validationResult.Success)
                return validationResult;

            Result getMovieInfoResult = await FillMovieInfo(item);
            if (!getMovieInfoResult.Success)
                return new Result<ShowTime>(getMovieInfoResult.Code, getMovieInfoResult.Message);

            ShowtimeEntity updatedEntity = _showtimesRepository.Update(_mapper.Map<ShowtimeEntity>(item));
            await _unitOfWork.SaveChangesAsync();
            return new Result<ShowTime>(ResultCode.Ok) {
                Data = _mapper.Map<ShowTime>(updatedEntity)
            };
        }

        #region private Methods
        private async Task<Result> FillMovieInfo(ShowTime item) {
            Result<ImdbMovie> movieInfo = await _imdbService.GetAsync(item.Movie.ImdbId);
            if (!movieInfo.Success)
                return new Result(movieInfo.Code, movieInfo.Message);

            item.Movie = _mapper.Map<Movie>(movieInfo.Data);

            return new Result(ResultCode.Ok);
        }
        #endregion

    }
}
