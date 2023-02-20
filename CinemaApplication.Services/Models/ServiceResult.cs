namespace CinemaApplication.Services.Models
{
    public enum ServiceResultType
    {
        Success,
        Failed
    }

    public class ServiceResult
    {
        public bool IsSuccessful { get; }
        public string Error { get; }

        public ServiceResult(bool isSuccessful)
            : this(isSuccessful, string.Empty)
        { }

        public ServiceResult(bool isSuccessful, string error)
        {
            this.IsSuccessful = isSuccessful;
            this.Error = error;
        }

        public static ServiceResult Success
            => new ServiceResult(true);

        public static ServiceResult Failure(string error)
            => new ServiceResult(false, error);

        public static implicit operator ServiceResult(bool result)
            => new ServiceResult(result);

        public bool IsError => !IsSuccessful;
    }
}
