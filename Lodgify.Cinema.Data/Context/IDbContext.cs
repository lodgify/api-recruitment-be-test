using Lodgify.Cinema.Domain.Entitie;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lodgify.Cinema.Infrastructure.Data.Context
{
    public interface IDbContext : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken));
        DbSet<AuditoriumEntity> Auditoriums { get; set; }
        DbSet<ShowtimeEntity> Showtimes { get; set; }
        DbSet<MovieEntity> Movies { get; set; }
    }
}