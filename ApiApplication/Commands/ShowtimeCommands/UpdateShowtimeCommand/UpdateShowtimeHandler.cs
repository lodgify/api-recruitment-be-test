using ApiApplication.Database;
using ApiApplication.Database.Entities;
using ApiApplication.Resources;
using AutoMapper;
using IMDbApiLib;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Commands.ShowtimeCommands.UpdateShowtimeCommand
{
    public class UpdateShowtimeHandler : IRequestHandler<UpdateShowtimeRequest, Showtime>
    {
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly IMapper _mapper;

        public UpdateShowtimeHandler(IShowtimesRepository showtimesRepository, IMapper mapper)
        {
            _showtimesRepository = showtimesRepository;
            _mapper = mapper;
        }

        public async Task<Showtime> Handle(UpdateShowtimeRequest request, CancellationToken cancellationToken)
        {
            var showtime = _mapper.Map<ShowtimeEntity>(request.Showtime);
            if (showtime.Movie != null) 
            {
                var apiLib = new ApiLib("k_ai05h4st");
                var result = await apiLib.TitleAsync(showtime.Movie.ImdbId);
                showtime.Movie.Title = result.Title;
                showtime.Movie.Stars = result.Stars;
                showtime.Movie.ReleaseDate = DateTime.Parse(result.ReleaseDate);
            }

            var showtimeUpdated = _showtimesRepository.Update(showtime);

            return _mapper.Map<Showtime>(showtimeUpdated);

        }
    }
}
