using System;
using Xunit;
using ApiApplication.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ApiApplication.Controllers;
using AutoMapper;
using ApiApplication;
using Moq;
using ApiApplication.Services.Facade;
using ApiApplication.Services;
using ApiApplication.IMDb;
using System.Threading.Tasks;
using ApiApplication.Database.Entities;
using System.Linq;
using System.Collections.Generic;
using ApiApplication.DTO;

namespace ApiRecruimentIntegrationTest
{
    public class ShowTimeRepositoryTest
    {

        private IShowtimesRepository _repo;
        private CreateCinemaMemoryDatabase database = new CreateCinemaMemoryDatabase();

        [Fact]
        public void UpdateShowTime_ExistInDatabase_ShouldUpdateToDatabase()
        {
            // Arrange
            database = new CreateCinemaMemoryDatabase();
            var _context = database.CreateDatabase("UpdateShowTime_ExistInDatabase_ShouldUpdateToDatabase");
            _repo = new ShowtimesRepository(_context);

            _context.Auditoriums.Add(TestData.TestData.CreateAuditoriumEntityWithShowtimeWithMovie(1, 2));

            _context.SaveChanges();

            var showtimeToUpdate = _context.Showtimes.Find(2);

            showtimeToUpdate.StartDate = DateTime.Now;

            //Act

            _repo.Update(showtimeToUpdate);


            var res = _context.Showtimes.Find(2);


            // Assert
            Assert.Equal(res.StartDate.ToString(), showtimeToUpdate.StartDate.ToString());

        }

        [Fact]
        public void AddShowTime_NoExistDatabase_ShouldAddToDatabase()
        {
            // Arrange
            database = new CreateCinemaMemoryDatabase();
            var _context = database.CreateDatabase("AddShowTime_NoExistDatabaseShouldAddToDatabase");
            _repo = new ShowtimesRepository(_context);
            //Act
            _repo.Add(TestData.TestData.CreateShowtimeWithMovie(1));
            var res = _context.Showtimes.Find(1);

            // Assert
            Assert.NotNull(res);

        }


        [Fact]
        public void DeleteShowTime_ExistDatabase_ShouldDeleteToDatabase()
        {
            // Arrange
            database = new CreateCinemaMemoryDatabase();
            var _context = database.CreateDatabase("DeleteShowTime_ExistDatabase_ShouldDeleteToDatabase");
            _repo = new ShowtimesRepository(_context);
            _context.Auditoriums.Add(TestData.TestData.CreateAuditoriumEntityWithShowtimeWithMovie(1, 1));

            _context.SaveChanges();

            var showtimeToDelete = _context.Showtimes.Find(1);

            //Act
            _repo.Delete(1);
            var res = _context.Showtimes.Find(1);

            // Assert
            Assert.Null(res);
        }


        [Fact]
        public void GetByMovieByTitle_ExistDatabase_ShouldReturnShowTimeToDatabase()
        {
            // Arrange
            database = new CreateCinemaMemoryDatabase();
            var _context = database.CreateDatabase("GetByMovieByTitle_ExistDatabase_ShouldReturnShowTimeToDatabase");
            _repo = new ShowtimesRepository(_context);

            var dataAuditoriun = TestData.TestData.CreateAuditoriumEntityWithShowtimeWithMovie(1, 1);

            var showtimeNew = TestData.TestData.CreateShowtimeWithMovie(2);
            var newMoview = TestData.TestData.CreateMovie(3);
            var newTitle = "test";
            newMoview.Title = newTitle;
            showtimeNew.Movie = newMoview;
            dataAuditoriun.Showtimes.Add(showtimeNew);

            _context.Auditoriums.Add(dataAuditoriun);

            _context.SaveChanges();

            Func<IQueryable<MovieEntity>, bool> filterfunction = source =>
            {
                var a = source.Where(p => p.Title == newTitle);

                return a.Any();

            };

            //Act
            var res = _repo.GetByMovie(filterfunction);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(res.Id, showtimeNew.Id);

        }


        [Fact]
        public void GetSchedulebyTitle_ExistDatabase_ShouldReturnScheduleToDatabase()
        {
            // Arrange
            database = new CreateCinemaMemoryDatabase();
            var _context = database.CreateDatabase("GetSchedulebyTitle_ExistDatabase_ShouldReturnScheduleToDatabase");
            _repo = new ShowtimesRepository(_context);

            var dataAuditoriun = TestData.TestData.CreateAuditoriumEntityWithShowtimeWithMovie(1, 1);

            var showtimeNew = TestData.TestData.CreateShowtimeWithMovie(2);
            var newMoview = TestData.TestData.CreateMovie(3);
            var newTitle = "test";
            newMoview.Title = newTitle;
            showtimeNew.Movie = newMoview;
            dataAuditoriun.Showtimes.Add(showtimeNew);
            IEnumerable<string> newSchedule = new List<string>() { "23:00" };
            showtimeNew.Schedule = newSchedule;

            _context.Auditoriums.Add(dataAuditoriun);

            _context.SaveChanges();

            Func<IQueryable<ScheduleDTO>, bool> filterFunctionTitle = source =>
            {
                var a = source.Where(p => p.Title == newTitle);
                return a.Any();

            };

            ////Act
            var res = _repo.GetSchedule(filterFunctionTitle);

            // Assert
            Assert.NotNull(res);
            Assert.Equal(res.FirstOrDefault().Title, showtimeNew.Movie.Title);
            Assert.Equal(res.FirstOrDefault().Schedule.FirstOrDefault().ToString(),
                newSchedule.FirstOrDefault().ToString());

        }


        [Fact]
        public void GetCollectionById_ExistDatabase_ShouldReturnCollectionToDatabase()
        {
            // Arrange
            database = new CreateCinemaMemoryDatabase();
            var _context = database.CreateDatabase("GetCollectionById_ExistDatabase_ShouldReturnCollectionToDatabase");
            _repo = new ShowtimesRepository(_context);

            var dataAuditoriun = TestData.TestData.CreateAuditoriumEntityWithShowtimeWithMovie(1, 1);
            var showtimeNew = TestData.TestData.CreateShowtimeWithMovie(2,2);
            dataAuditoriun.Showtimes.Add(showtimeNew);

            _context.Auditoriums.Add(dataAuditoriun);

            _context.SaveChanges();

            Func<IQueryable<ShowtimeEntity>, bool> filterFunctionId = source =>
            {
                var a = source.Where(p => p.Id == showtimeNew.Id);
                return a.Any();

            };

            //Act
            var res = _repo.GetCollection(filterFunctionId);

            // Assert
            Assert.NotNull(res);          
            Assert.Equal(res.FirstOrDefault().Id,
                showtimeNew.Id);
        }
    }
}
