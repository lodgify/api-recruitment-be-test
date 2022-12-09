using System;

namespace ApiApplication.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string type) : base($"Unable to find any {type}") { }

        public NotFoundException(string type, string fieldName, string fieldValue)
            : base($"Unable to find a {type} with {fieldName} {fieldValue}")
        {
        }

        public NotFoundException(DateTime date, string messageToMatch)
           : base($"The date {date} does not match {messageToMatch}")
        {
        }

        public NotFoundException(string type, int id)
           : base($"Unable to find a {type} with id {id}")
        {
        }

    }
}
