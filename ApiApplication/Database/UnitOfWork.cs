using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Database {
    public class UnitOfWork : IUnitOfWork {
        private readonly CinemaContext _cinemaContext;

        public UnitOfWork(CinemaContext cinemaContext) {
            _cinemaContext = cinemaContext;
        }

        public Task SaveChangesAsync(CancellationToken cancellationToken = default) =>
            _cinemaContext.SaveChangesAsync(cancellationToken);
    }
}
