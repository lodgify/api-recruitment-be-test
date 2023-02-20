namespace CinemaApplication.Services.Models
{
    public class ServiceDataResult<TData> : ServiceResult
    {
        public TData Data { get; }

        private ServiceDataResult(ServiceResultType serviceResult, string error)
            : base(serviceResult, error)
        { }

        public ServiceDataResult(TData data)
            : base(ServiceResultType.Success)
        {
            this.Data = data;
        }

        public static ServiceDataResult<TData> WithData(TData data)
            => new ServiceDataResult<TData>(data);

        public static ServiceDataResult<TData> WithError(string errorMessage)
            => new ServiceDataResult<TData>(ServiceResultType.Failed, errorMessage);
    }
}
