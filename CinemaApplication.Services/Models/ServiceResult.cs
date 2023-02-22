namespace CinemaApplication.Services.Models
{
    public class ServiceResult
    {
        public ServiceResultType Status { get; }
        public string Error { get; }

        public ServiceResult(ServiceResultType status)
            : this(status, string.Empty)
        { }

        public ServiceResult(ServiceResultType status, string error)
        {
            this.Status = status;
            this.Error = error;
        }

        public static ServiceResult Success
            => new ServiceResult(ServiceResultType.Success);

        public static ServiceResult Failure(string error)
            => new ServiceResult(ServiceResultType.Failed, error);

        public static ServiceResult NotFound(string error) => new ServiceResult(ServiceResultType.NotFound, error);

        public static implicit operator ServiceResult(bool isSucessful)
            => new ServiceResult(isSucessful ? ServiceResultType.Success : ServiceResultType.Failed);

        public bool IsError => Status != ServiceResultType.Success;

        public bool IsSuccess => Status == ServiceResultType.Success;
    }
}
