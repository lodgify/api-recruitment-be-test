using ApiApplication.DTOs.API;
using ApiApplication.WebClients;
using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Infraestructure.Database.Specifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ApiApplication.Services
{
    public class ShowtimeService : IShowtimeService
    {
        private readonly IShowtimesRepository _repository;
        private readonly IIMDBWebApiClient _webClient;
        private readonly IMapper _mapper;

        public ShowtimeService(IShowtimesRepository repository, IIMDBWebApiClient webClient, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _webClient = webClient ?? throw new ArgumentNullException(nameof(webClient));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<ShowtimeEntity>> GetAsync(DateTime date = default, string movieTitle = null)
        {
            IEnumerable<ShowtimeEntity> entities;

            if (date == default && movieTitle == null)
            {
                entities = await _repository.GetCollectionAsync();
            }
            else
            {
                var specification = new AndSpecification<ShowtimeEntity>();
                if (date != default)
                {
                    specification.AddExpression(entity => date >= entity.StartDate && date <= entity.EndDate);
                }
                if (movieTitle != null)
                {
                    specification.AddExpression(entity => entity.Movie.Title.ToUpper() == movieTitle.ToUpper());
                }

                entities = await _repository.GetCollectionAsync(specification.QueryExpression);
            }

            return entities;
        }

        public async Task<ShowtimeEntity> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<ShowtimeEntity> CreateAsync(Showtime showtime)
        {
            var showtimeEntity = _mapper.Map<ShowtimeEntity>(showtime);

            var movieInfo = await _webClient.GetMovieInfoAsync(showtime.Movie.ImdbId);
            showtimeEntity.Movie = _mapper.Map<MovieEntity>(movieInfo);

            return await _repository.AddAsync(showtimeEntity);
        }

        public async Task<ShowtimeEntity> UpdateAsync(Showtime showtime)
        {
            var existingEntity = await _repository.GetByIdAsync(showtime.Id);
            _mapper.Map(showtime, existingEntity);

            if (showtime.Movie != null && showtime.Movie.ImdbId != existingEntity.Movie.ImdbId)
            {
                var movieInfo = await _webClient.GetMovieInfoAsync(showtime.Movie.ImdbId);
                existingEntity.Movie = _mapper.Map<MovieEntity>(movieInfo);
            }

            return await _repository.UpdateAsync(existingEntity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }        
    }
}
