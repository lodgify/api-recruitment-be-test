using System;
namespace ApiApplication.Exceptions
{
    public class MovieNotFoundException : DomainException
    {
        public MovieNotFoundException()
        {
        }

        public override string Message => "The movie/show is not found";

        public override string ErrorCode => "M002";
    }
}

