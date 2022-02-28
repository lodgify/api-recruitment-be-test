using Cinema.Core.Utilities.Results;
using Cinema.Entities.Concrete;
using Cinema.Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Business.Abstract
{
    public interface IShowtimeService
    {

        Task<IDataResult<List<ShowtimeDto>>> GetShowtimes( string movieTitle = null, DateTime? date = null);
        Task<IResult> AddAsync(ShowtimeDto showtime);
        Task<IResult> UpdateAsync(int id ,ShowtimeDto showtime);
        Task<IResult> DeleteAsync(int id);


    }
}
