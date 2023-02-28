using System;

namespace Lodgify.Cinema.Domain.Contract
{
    public interface IImdbStatus
    {
        bool IsUp { get; }
        DateTime LastStatusCheck { get; }
        Exception LastException { get; }
        DateTime LastExceptionThrow { get; }
        void SetStatus(bool newStatus, DateTime checkTime);
        void SetException(Exception ex, DateTime exceptionTime);
    }
}