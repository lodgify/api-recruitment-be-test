using System;
using ApiApplication.Database;
using ApiApplication.Resources;
using AutoMapper;
using Moq;

namespace ApiApplication.Tests.Shared
{
    public abstract class UnitTestCaseBase
    {
        protected Mock<IShowtimesRepository> showTimeRepository { get; private set; }
        protected Mock<IImdbRepository> imdbRepository { get; private set; }
        protected Mock<IMovieRepository> movieRepository { get; private set; }
        protected IMapper mapper { get; private set; }

        protected UnitTestCaseBase()
        {
            this.showTimeRepository = new Mock<IShowtimesRepository>();
            this.imdbRepository = new Mock<IImdbRepository>();
            this.movieRepository = new Mock<IMovieRepository>();
            var mappingConfig = new MapperConfiguration(c => c.AddProfile(new ShowTimeMapProfile()));
            this.mapper = mappingConfig.CreateMapper();
        }
    }
}

