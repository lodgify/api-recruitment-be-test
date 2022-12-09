using ApiApplication.Database;
using ApiApplication.Resources;
using AutoMapper;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Commands.ShowtimeCommands.DeleteShowtimeCommand
{
    public class DeleteShowtimeHandler : IRequestHandler<DeleteShowtimeRequest, Showtime>
    {
        private readonly IShowtimesRepository _showtimesRepository;
        private readonly IMapper _mapper;

        public DeleteShowtimeHandler(IShowtimesRepository showtimesRepository, IMapper mapper)
        {
            _showtimesRepository = showtimesRepository;
            _mapper = mapper;
        }

        public async Task<Showtime> Handle(DeleteShowtimeRequest request, CancellationToken cancellationToken)
        {
            var showtimeDeleted = _showtimesRepository.Delete(request.Id);

            return _mapper.Map<Showtime>(showtimeDeleted);

        }
    }
}
