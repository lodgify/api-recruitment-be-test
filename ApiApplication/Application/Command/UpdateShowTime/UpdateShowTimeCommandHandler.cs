using ApiApplication.Application.Commands;
using ApiApplication.Core.Base;
using Lodgify.Cinema.Domain.Contract;
using Lodgify.Cinema.Domain.Contract.Repositorie;
using Lodgify.Cinema.Domain.Entitie;
using Lodgify.Cinema.Domain.Notification;
using Lodgify.Cinema.Domain.Resources;
using Lodgify.Cinema.Infrastructure.Data.Context;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Lodgify.Cinema.Domain.Dto.ImdbRepository;

namespace ApiApplication.Application.Command
{
    public sealed class UpdateShowTimeCommandHandler : BaseCommandHandler, IUpdateShowTimeCommandHandler
    {
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly IDomainNotification _domainNotification;
        private readonly IImdbRepository _imdbRepository;
        private readonly IImdbIdTranslatorService _imdbIdTranslatorService;

        public UpdateShowTimeCommandHandler(IShowtimesRepository showtimesRepository,
            CinemaContext dbContext,
            IImdbRepository imdbRepository,
            IImdbIdTranslatorService imdbIdTranslatorService,
            IDomainNotification domainNotification) : base(dbContext, domainNotification)
        {
            _showtimesRepository = showtimesRepository;
            _domainNotification = domainNotification;
            _imdbIdTranslatorService = imdbIdTranslatorService;
            _imdbRepository = imdbRepository;
        }

        /// <summary>
        /// 1375666 - test imdb id
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<UpdateShowTimeResponse> ExecuteAsync(UpdateShowTimeRequest command, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async () =>
            {
                var showTime = await _showtimesRepository.GetByIDAsync(command.Id, cancellationToken);
                MovieResponse newMovie = null;

                if (showTime == null)
                {
                    _domainNotification.Add(string.Format(BusinessMessage.ShowTimeNotFoundById, command.Imdb_id));
                    return null;
                }

                bool isMovieChanged = command.Imdb_id.HasValue
                                   && (
                                          showTime.Movie ==null 
                                        ||command.Imdb_id.Value != _imdbIdTranslatorService.Get(showTime.Movie.ImdbId)
                                     );
                if (isMovieChanged)
                {
                    newMovie = await _imdbRepository.GetMovieByIdAsync(command.Imdb_id.Value, cancellationToken);
                    if (newMovie == null)
                    {
                        _domainNotification.Add(string.Format(BusinessMessage.MovieNotFoundById, command.Imdb_id));
                        return null;
                    }
                }

                showTime = GetShowTimeEntitie(command, newMovie, showTime);
                _showtimesRepository.Update(showTime);
                return new UpdateShowTimeResponse();
            });
        }

        public ShowtimeEntity GetShowTimeEntitie(UpdateShowTimeRequest command, MovieResponse movie, ShowtimeEntity showTimeEntity)
        {
            showTimeEntity.AuditoriumId = command.AuditoriumId ?? showTimeEntity.AuditoriumId;
            showTimeEntity.EndDate = command.EndDate ?? showTimeEntity.EndDate;
            showTimeEntity.StartDate = command.StartDate ?? showTimeEntity.StartDate;
            showTimeEntity.Schedule = command.Schedule ?? showTimeEntity.Schedule;

            //ToDo - Take care to not duplicate Movies in dabatase
            if (movie != null)
                showTimeEntity.Movie = new MovieEntity
                {
                    ImdbId = _imdbIdTranslatorService.Get(command.Imdb_id.Value),
                    ReleaseDate = movie.release_year.HasValue ? new DateTime(movie.release_year.Value, 1, 1):DateTime.MinValue,
                    Stars = movie.rating.ToString(),
                    Title = movie.title,
                };

            return showTimeEntity;
        }
    }
}