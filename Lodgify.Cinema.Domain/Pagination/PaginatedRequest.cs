namespace Lodgify.Cinema.Domain.Pagination
{
    public class PaginatedRequest : IPaginatedRequest
    {
        public const int DefaultSinceIfZero = 0;
        public const int DefaultPageSizeIfZero = 20;

        #region [prop]
        public long LastSince { get; set; }

        public long Since { get; private set; }

        public int PageSize { get; private set; }

        #endregion [prop]

        #region [ctor]

        public PaginatedRequest(long since, int pageSize)
        {
            ConfigurePagination(since, pageSize);
        }

        #endregion [ctor]

        private long CalculateAutomaticSince => Since + PageSize;

        public void SetPagination(long since, int pageSize)
        {
            ConfigurePagination(since, pageSize);
        }

        private void ConfigurePagination(long since, int pageSize)
        {
            Since = since <= 0 ? DefaultSinceIfZero : since;
            PageSize = pageSize <= 10 || pageSize >= 50 ? DefaultPageSizeIfZero : pageSize;
            LastSince = CalculateAutomaticSince;
        }

        public void SetLastSince(long since)
        {
            this.LastSince = since;
        }
    }
}
