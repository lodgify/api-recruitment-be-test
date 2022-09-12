using ApiApplication.Database.Entities;
using ApiApplication.Services;
using ApiApplication.Tests.Shared;
using Moq;

namespace ApiApplication.Tests;

[TestClass]
public class CreateShowtimeTests : UnitTestCaseBase
{
    private readonly ShowtimeService service;

    public CreateShowtimeTests()
    {
        this.service = new ShowtimeService(this.mapper, this.showTimeRepository.Object, this.imdbRepository.Object, this.movieRepository.Object);
    }

    [TestMethod]
    public async Task ItShouldCreateShowtimeWithNoIssues()
    {
        var showId = 100;
        var imdbId = "abc100";
        var movie = MovieMother.Create(imdbId:"abc100", Title:"The most important test");
        var show = ShowtimeMother.Create(Id:showId,movie);
        var imdbTitle = ImdbTitleMother.Create(imdbId: imdbId);
        var entity = this.mapper.Map<ShowtimeEntity>(show);
        this.imdbRepository.Setup(m => m.GetTitle(show.Movie.ImdbId)).ReturnsAsync(imdbTitle);
        this.showTimeRepository.Setup(x => x.Add(entity)).ReturnsAsync(entity);

        var result = await service.CreateShowtime(show);

        Assert.AreEqual(100, result.Id);
        Assert.AreEqual("abc100", result.Movie.ImdbId);


    }
}
