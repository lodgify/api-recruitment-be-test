namespace ApiApplication.Core.Pagination
{
    public interface IPaginatedRequest
    {
        long Since { get; }
        int PageSize { get; }
        long LastSince { get; }
        void SetPagination(long since, int pageSize);

        void SetLastSince(long since);
    }
}
