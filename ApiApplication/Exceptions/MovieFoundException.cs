using System;
namespace ApiApplication.Exceptions
{
    public class MovieFoundException: DomainException
    {
        public MovieFoundException()
        {
        }

        public override string Message => "There is an already existing movie";

        public override string ErrorCode => "M001";
    }
}

