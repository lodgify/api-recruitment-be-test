namespace ApiApplication.Models {
    public class Result {
        public Result(ResultCode code, string message = null) {
            Message = message;
            Code = code;
        }

        public string Message { get; set; }
        public ResultCode Code { get; set; }
        public bool Success => Code == ResultCode.Ok;
    }

    public class Result<T> : Result {
        public Result(T data, ResultCode code, string message = null)
            : base(code, message) {
            Data = data;
        }

        public Result(ResultCode code, string message = null) : base(code, message) {
        }

        public T Data { get; set; }
    }
}
