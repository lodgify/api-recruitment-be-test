namespace ApiApplication.Dtos.Base
{
    public class BaseResponseDto : BaseResponseDto<object>
    {
        public static BaseResponseDto SuccessResult(
            object result
        )
        {
            return new BaseResponseDto
            {
                Success = true,
                Error = default,
                Result = result
            };
        }

        public static BaseResponseDto ErrorResult(
            int statusCode,
            string statusType,
            string errorMessage
        )
        {
            return new BaseResponseDto
            {
                Success = false,
                Error = new ErrorDto
                {
                    StatusCode = statusCode,
                    StatusType = statusType,
                    ErrorMessage = errorMessage
                },
                Result = default
            };
        }
    }

    public class BaseResponseDto<T>
    {
        public bool Success { get; set; }
        public ErrorDto Error { get; set; }
        public T Result { get; set; }

        public static BaseResponseDto<P> SuccessResult<P>(
            P result
        )
        {
            return new BaseResponseDto<P>
            {
                Success = true,
                Error = default,
                Result = result
            };
        }

        public static BaseResponseDto<P> ErrorResult<P>(
            int statusCode,
            string statusType,
            string errorMessage
        )
        {
            return new BaseResponseDto<P>
            {
                Success = false,
                Error = new ErrorDto
                {
                    StatusCode = statusCode,
                    StatusType = statusType,
                    ErrorMessage = errorMessage
                },
                Result = default
            };
        }
    }
}
