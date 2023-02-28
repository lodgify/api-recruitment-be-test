using FluentAssertions;
using Lodgify.Cinema.Domain.Pagination;
using Lodgify.Cinema.DomainService.Pagination;
using Xunit;

namespace Lodgify.Cinema.UnitTest
{
    public class LogTest
    {
        [Trait("Data", "PaginatedRequestClass")]
        [Theory]
        [InlineData(1, 20)]
        [InlineData(10, 20)]
        [InlineData(100, 20)]
        [InlineData(1000, 20)]
        public void PaginatedRequestCreateSuccess(int since, int pageSize)
        {
            //Arranje/Act
            IPaginatedRequest paginatedRequest = new PaginatedRequest(since, pageSize);

            //Assert
            paginatedRequest.Since.Should().Be(since);
            paginatedRequest.PageSize.Should().Be(pageSize);
            paginatedRequest.LastSince.Should().Be(since + pageSize);
        }

        [Trait("Data", "PaginatedRequestClass")]
        [Theory]
        [InlineData(1, 20, 20)]
        [InlineData(10, 20, 30)]
        [InlineData(100, 20, 120)]
        [InlineData(1000, 20, 1020)]
        public void PaginatedRequestSetPaginationSuccess(int since, int pageSize, int lastSince)
        {
            //Arranje
            IPaginatedRequest paginatedRequest = new PaginatedRequest(0, 0);

            //Act
            paginatedRequest.SetPagination(since, pageSize);
            paginatedRequest.SetLastSince(lastSince);

            //Assert
            paginatedRequest.Since.Should().Be(since);
            paginatedRequest.PageSize.Should().Be(pageSize);
            paginatedRequest.LastSince.Should().Be(lastSince);
        }

        [Trait("Data", "PaginatedRequestClass")]
        [Theory]
        [InlineData(0, 0)]
        [InlineData(-1, -1)]
        [InlineData(-1, 50)]
        public void PaginatedRequestCreateZeroBasedSuccess(int since, int pageSize)
        {
            //Arranje/Act
            IPaginatedRequest paginatedRequest = new PaginatedRequest(since, pageSize);

            //Assert
            paginatedRequest.Since.Should().Be(PaginatedRequest.DefaultSinceIfZero);
            paginatedRequest.PageSize.Should().Be(PaginatedRequest.DefaultPageSizeIfZero);
            paginatedRequest.LastSince.Should().Be(PaginatedRequest.DefaultSinceIfZero + PaginatedRequest.DefaultPageSizeIfZero);
        }


        [Trait("Data", "PaginatedRequestClass")]
        [Theory]
        [InlineData(0, 0)]
        [InlineData(-1, -1)]
        public void PaginatedRequestSetPaginationCreateZeroBasedSuccess(int since, int pageSize)
        {
            //Arranje
            IPaginatedRequest paginatedRequest = new PaginatedRequest(0, 0);

            //Act
            paginatedRequest.SetPagination(since, pageSize);

            //Assert
            paginatedRequest.Since.Should().Be(PaginatedRequest.DefaultSinceIfZero);
            paginatedRequest.PageSize.Should().Be(PaginatedRequest.DefaultPageSizeIfZero);
            paginatedRequest.LastSince.Should().Be(PaginatedRequest.DefaultSinceIfZero + PaginatedRequest.DefaultPageSizeIfZero);
        }
    }
}
