using System.Collections.Generic;

namespace ApiApplication.Core.Notification
{
    public interface IDomainNotification
    {
        bool HasNotification { get; }

        IEnumerable<string> GetNotifications { get; }
        void AddRange(IEnumerable<string> notifications);
        void Add(string notification);
        void SetUniqueNotification(string notification);
        void Clear();
    }
}
