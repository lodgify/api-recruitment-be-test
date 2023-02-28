using System;

namespace Lodgify.Cinema.Domain.Contract
{
    public interface IImdbStatus
    {
        bool IsUp { get; }
        DateTime? LastStatusCheck { get; }
        string LastException { get; }
        DateTime? LastExceptionThrow { get; }
        void SetCheck(bool newStatus, DateTime checkTime);
        void SetException(Exception ex, DateTime exceptionTime);
        bool FirstCheckDone { get; }
    }
}