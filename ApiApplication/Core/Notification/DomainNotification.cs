using System.Collections.Generic;
using System.Linq;

namespace ApiApplication.Core.Notification
{
    public sealed class DomainNotification : IDomainNotification
    {
        #region [prop]
        public bool HasNotification => _notifications != null && _notifications.Any();

        private IList<string> _notifications;

        private IList<string> Notifications
        {
            get
            {
                if (_notifications == null)
                    _notifications = new List<string>();

                return _notifications;
            }
        }

        #endregion [prop]

        public IEnumerable<string> GetNotifications => Notifications;

        public void Add(string notification)
        {
            if (!Notifications.Any(w => w == notification))
                Notifications.Add(notification);
        }

        public void SetUniqueNotification(string notification)
        {
            Notifications.Clear();
            Notifications.Add(notification);
        }

        public void AddRange(IEnumerable<string> notifications)
        {
            foreach (var notification in notifications)
                Add(notification);
        }
        public void Clear() => Notifications.Clear();

    }
}
