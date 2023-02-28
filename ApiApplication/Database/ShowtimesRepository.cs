using ApiApplication.Database.Entities;
using ApiApplication.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiApplication.Database
{
    public class ShowtimesRepository : IShowtimesRepository
    {
        private readonly CinemaContext _context;
        public ShowtimesRepository(CinemaContext context)
        {
            _context = context;
        }

        public ShowtimeEntity Add(ShowtimeEntity showtimeEntity)
        {
            var showtime = _context.Showtimes.Find(showtimeEntity.Id);

            if (showtime == null)
            {
                _context.Showtimes.Add(showtimeEntity);
            }
            else
            {
                throw new Exception(Constants.Exception.CanNotInsertShowtimeDuplicatId);
            }

            _context.SaveChanges();

            var showtimeAdded = _context.Showtimes.Find(showtimeEntity.Id);

            return showtimeAdded;
        }

        public ShowtimeEntity Delete(int id)
        {
            var entityToDelete = _context.Showtimes.Find(id);

            if (entityToDelete != null)
            {
                _context.Showtimes.Remove(entityToDelete);
                _context.SaveChanges();
            }
            else
            {
                throw new Exception(Constants.Exception.CanNotDeleteShowtimeNotFound);

            }

            return entityToDelete;
        }

        public ShowtimeEntity GetByMovie(Func<IQueryable<MovieEntity>, bool> filter)
        {
            var query = from g in _context.Showtimes.ToList()
                        join mg in _context.Movies.ToList() on g.Id equals mg.ShowtimeId
                        where (filter(_context.Movies.Where(t => t.Id == mg.Id).AsQueryable()))
                        select g;


            return query.ToList().FirstOrDefault();
        }

        public IEnumerable<ShowtimeEntity> GetCollection()
        {
            return _context.Showtimes.ToList();
        }

        public IEnumerable<ShowtimeEntity> GetCollection(Func<IQueryable<ShowtimeEntity>, bool> filter)
        {
            var query = from g in _context.Showtimes.ToList()
                        where (filter(_context.Showtimes.Where(t => t.Id == g.Id).AsQueryable()))
                        select g;

            return query.ToList();
        }

        public IEnumerable<ScheduleViewModel> GetSchedule(Func<IQueryable<ScheduleViewModel>, bool> filter)
        {

            var queryFilter = from g in _context.Showtimes.AsNoTracking()
                              join mg in _context.Movies.AsNoTracking() on g.Id equals mg.ShowtimeId
                              select new ScheduleViewModel
                              {
                                  Id = g.Id,
                                  Title = mg.Title,
                                  StartDate = g.StartDate,
                                  EndDate = g.EndDate,
                                  Schedule = g.Schedule

                              };

            var query = from q in queryFilter.ToList()
                        where (filter(queryFilter.Where(t => t.Id == q.Id).AsQueryable()))
                        select q;



            return query.ToList();
        }

        public ShowtimeEntity Update(ShowtimeEntity showtimeEntity)
        {

            var showtime = _context.Showtimes.Include(i => i.Movie).
                FirstOrDefault(p => p.Id == showtimeEntity.Id);

            if (showtime == null)
            {
                throw new Exception(Constants.Exception.CanNotUpdateShowtimeNotFound);
            }

            UpdateData(showtime, showtimeEntity);
            _context.Showtimes.Update(showtime);

            _context.SaveChanges();

            var showtimeUpdated = _context.Showtimes.Find(showtimeEntity.Id);

            return showtimeUpdated;
        }

        private void UpdateData(ShowtimeEntity showtimeEntity, ShowtimeEntity newShowtimeEntity)
        {
            showtimeEntity.StartDate = newShowtimeEntity.StartDate;
            showtimeEntity.EndDate = newShowtimeEntity.EndDate;
            showtimeEntity.Schedule = newShowtimeEntity.Schedule;
            showtimeEntity.AuditoriumId = newShowtimeEntity.AuditoriumId;
            showtimeEntity.Movie.Title = newShowtimeEntity.Movie.Title;
            showtimeEntity.Movie.Stars = newShowtimeEntity.Movie.Stars;
            showtimeEntity.Movie.ReleaseDate = newShowtimeEntity.Movie.ReleaseDate;
        }
    }
}
