using Lodgify.Cinema.Domain.Contract;
using System;

namespace Lodgify.Cinema.DomainService.Imdb
{
    public class ImdbStatus : IImdbStatus
    {
        public ImdbStatus(bool isUp, DateTime? checkDate)
        {
            IsUp = isUp;
            LastStatusCheck = checkDate;
        }

        public bool IsUp { get; private set; }
        public bool  FirstCheckDone { get; private set; }

        public DateTime? LastStatusCheck { get; private set; }

        public Exception LastException { get; private set; }

        public DateTime? LastExceptionThrow { get; private set; }

        public void SetException(Exception ex, DateTime exceptionTime)
        {
            LastException = ex;
            LastExceptionThrow = exceptionTime;
        }

        public void SetCheck(bool newStatus, DateTime checkTime)
        {
            IsUp = newStatus;
            LastStatusCheck = checkTime;
            FirstCheckDone = true;
        }
    }
}