using ApiApplication;
using ApiApplication.Database;
using ApiApplication.IMDb;
using ApiApplication.Services;
using ApiApplication.Services.Facade;
using AutoMapper;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ApiRecruimentIntegrationTest
{
    public class ShowTimeServiceTest
    {

        private IMapper _mapper;
        private Mock<IImdbFacade> _mockServiceFacade;
        private IShowtimesRepository _repo;
        private IShowtimeService _service;
        private CreateCinemaMemoryDatabase database = new CreateCinemaMemoryDatabase();

        public ShowTimeServiceTest()
        {
            var Mapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfig());
            });

            _mapper = Mapper.CreateMapper();
            _mockServiceFacade = new Mock<IImdbFacade>();

            var movieImdb = TestData.TestData.CreateMovie();
            _mockServiceFacade.Setup(x => x.DiscoverMovie(It.IsAny<CriteriaImdb>())).ReturnsAsync(movieImdb);
        }


        [Fact]
        public async Task AddShowTime_NoExistDatabase_ShouldAddToDatabase()
        {
            // Arrange
            var _context = database.CreateDatabase("AdShowTime_NoExistDatabase_ShouldAddToDatabase");
            _repo = new ShowtimesRepository(_context);
            _service = new ShowtimeService(_repo, _mockServiceFacade.Object, _mapper);
            //Act
            await _service.Add(TestData.TestData.CreateCommandShowTime(1));
            var res = _context.Showtimes.Find(1);

            // Assert
            Assert.NotNull(res);

        }


        [Fact]
        public async Task UpdateShowTime_ExistDatabase_ShouldUpdateToDatabase()
        {
            //Arrange
            var _context = database.CreateDatabase("UpdateShowTime_ExistDatabase_ShouldUpdateToDatabase");
            _repo = new ShowtimesRepository(_context);
            _service = new ShowtimeService(_repo, _mockServiceFacade.Object, _mapper);

            _context.Auditoriums.Add(TestData.TestData.CreateAuditoriumEntityWithShowtimeWithMovie());

            _context.SaveChanges();

            var showtimeToUpdate = TestData.TestData.CreateCommandShowTime(1);

            showtimeToUpdate.Start_date = DateTime.Now;

            //Act

            await _service.Update(showtimeToUpdate);

            var res = _context.Showtimes.Find(1);

            //Assert
            Assert.Equal(res.StartDate.ToString(), showtimeToUpdate.Start_date.ToString());

        }

        [Fact]
        public void GetShowTimeSchedule_ExistDatabase_ShouldReturnSameScheduleFromDatabase()
        {
            //Arrange
            var _context = database.CreateDatabase("GetShowTimeSchedule_ExistDatabase_ShouldReturnSameScheduleToDatabase");
            _repo = new ShowtimesRepository(_context);
            _service = new ShowtimeService(_repo, _mockServiceFacade.Object, _mapper);

            var dataAuditoriun = TestData.TestData.CreateAuditoriumEntityWithShowtimeWithMovie();
            _context.Auditoriums.Add(dataAuditoriun);


            _context.SaveChanges();
            //Act

            var res = _service.GetShowTimeSchedule();

            var itemRes = res.ToList().FirstOrDefault().Schedule.ToList();
            var itemData = dataAuditoriun.Showtimes.ToList().FirstOrDefault().Schedule.ToList();


            //Assert
            Assert.NotNull(res);
            Assert.Equal(itemRes, itemData);
        }


        [Fact]
        public void GetShowTimeScheduleFilterByTitle_ExistDatabase_ShouldReturnSameTitleFromDatabase()
        {
            //Arrange
            var _context = database.CreateDatabase("GetShowTimeScheduleFilterByTitle_ExistDatabase_ShouldReturnSameTitleFromDatabase");
            _repo = new ShowtimesRepository(_context);
            _service = new ShowtimeService(_repo, _mockServiceFacade.Object, _mapper);

            var dataAuditoriun = TestData.TestData.CreateAuditoriumEntityWithShowtimeWithMovie();
            _context.Auditoriums.Add(dataAuditoriun);


            _context.SaveChanges();

            var itemTitle = dataAuditoriun.Showtimes.ToList().FirstOrDefault().Movie.Title;
            //Act

            var res = _service.GetShowTimeSchedule(itemTitle);

            var itemTitleRes = res.ToList().FirstOrDefault().Title;
            


            //Assert
            Assert.NotNull(res);
            Assert.Equal(itemTitleRes, itemTitle);
        }

    }
}
