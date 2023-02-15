using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.Resources;
using AutoMapper;
using IMDbApiLib;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Commands.ShowtimeCommands.CreateShowtimeCommand
{
    public class CreateShowtimeHandler : IRequestHandler<CreateShowtimeRequest, Showtime>
    {
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly IMapper _mapper;

        public CreateShowtimeHandler(IShowtimesRepository showtimesRepository, IMapper mapper)
        {
            _showtimesRepository = showtimesRepository;
            _mapper = mapper;
        }

        public async Task<Showtime> Handle(CreateShowtimeRequest request, CancellationToken cancellationToken)
        {
            var apiLib = new ApiLib("k_ai05h4st");
            var showtime = _mapper.Map<ShowtimeEntity>(request.Showtime);
            var result = await apiLib.TitleAsync(showtime.Movie.ImdbId);
            showtime.Movie.Title = result.Title;
            showtime.Movie.Stars = result.Stars;
            showtime.Movie.ReleaseDate = DateTime.Parse(result.ReleaseDate);

            var showtimeCreated = _showtimesRepository.Add(showtime);

            return _mapper.Map<Showtime>(showtimeCreated);

        }
    }
}
