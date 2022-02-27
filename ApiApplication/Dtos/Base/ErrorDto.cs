namespace ApiApplication.Dtos.Base
{
    public class ErrorDto
    {
        public int StatusCode { get; set; }
        public string StatusType { get; set; }
        public string ErrorMessage { get; set; }
    }
}
