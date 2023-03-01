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
    public sealed class AddShowTimeCommandHandler : BaseCommandHandler, IAddShowTimeCommandHandler
    {
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly IDomainNotification _domainNotification;
        private readonly IImdbRepository _imdbRepository;
        private readonly IImdbIdTranslatorService _imdbIdTranslatorService;
        public AddShowTimeCommandHandler(IShowtimesRepository showtimesRepository,
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
        public async Task<AddShowTimeResponse> ExecuteAsync(AddShowTimeRequest command, CancellationToken cancellationToken)
        {
            return await ExecuteAsync(async () =>
            {
                var movie = await _imdbRepository.GetMovieByIdAsync(command.Imdb_id, cancellationToken);

                if (movie == null)
                {
                    _domainNotification.Add(string.Format(BusinessMessage.MovieNotFoundById, command.Imdb_id));
                    return null;
                }

                if(command.Auditorium_Id <=0 || command.Auditorium_Id > 3)
                {
                    _domainNotification.Add(string.Format(BusinessMessage.InvalidAuditorium, command.Auditorium_Id));
                    return null;
                }

                var showTime = GetShowTimeEntitie(command, movie);
                await _showtimesRepository.AddAsync(showTime, cancellationToken);
                return new AddShowTimeResponse();
            });
        }

        public ShowtimeEntity GetShowTimeEntitie(AddShowTimeRequest command, MovieResponse movie)
        {
            return new ShowtimeEntity()
            {
                AuditoriumId = command.Auditorium_Id,
                EndDate = command.EndDate,
                StartDate = command.StartDate,
                Schedule = command.Schedule,
                Movie = new MovieEntity
                {
                    ImdbId = _imdbIdTranslatorService.Get(command.Imdb_id),
                    ReleaseDate = movie.release_year.HasValue ? new DateTime(movie.release_year.Value, 1, 1) : DateTime.MinValue,
                    Stars = movie.rating.ToString(),
                    Title = movie.title,
                }
            };
        }
    }
}