using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Database {
    public interface IUnitOfWork {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
