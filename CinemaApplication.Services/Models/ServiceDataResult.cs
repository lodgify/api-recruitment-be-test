namespace CinemaApplication.Services.Models
{
    public class ServiceDataResult<TData> : ServiceResult
    {
        public TData Data { get; }

        private ServiceDataResult(bool isSuccessful, string error)
            : base(isSuccessful, error)
        { }

        public ServiceDataResult(TData data)
            : base(true)
        {
            this.Data = data;
        }

        public static ServiceDataResult<TData> WithData(TData data)
            => new ServiceDataResult<TData>(data);

        public static ServiceDataResult<TData> WithError(string errorMessage)
            => new ServiceDataResult<TData>(false, errorMessage);
    }
}
