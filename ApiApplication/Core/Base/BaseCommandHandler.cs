using Lodgify.Cinema.Domain.Notification;
using Lodgify.Cinema.Infrastructure.Data.Context;
using System;
using System.Threading.Tasks;

namespace ApiApplication.Core.Base
{
    public abstract class BaseCommandHandler
    {
        #region [prop]

        private readonly IDomainNotification _domainNotification;
        private readonly IDbContext _context;
        protected bool ForceCommit { get; set; } = false;

        #endregion [prop]

        #region [ctor]

        public BaseCommandHandler(IDbContext context, IDomainNotification domainNotification)
        {
            _domainNotification = domainNotification;
            _context = context;
        }

        #endregion [ctor]

        protected async Task ExecuteAsync(Func<Task> action)
        {
            try
            {
                if (_domainNotification.HasNotification)
                    return;

                await action();

                if (!_domainNotification.HasNotification)
                    await _context.SaveChangesAsync();
            }
            finally
            {
                _context.Dispose();
            }

        }

        protected async Task<T> ExecuteAsync<T>(Func<Task<T>> function) where T : class
        {
            try
            {
                if (_domainNotification.HasNotification)
                    return null;

                var response = await function();

                if (_domainNotification.HasNotification)
                    return null;

                await _context.SaveChangesAsync();
                return response;
            }
            finally
            {
                _context.Dispose();
            }

        }
    }
}
