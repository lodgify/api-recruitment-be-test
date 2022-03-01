using Cinema.Core.DataAccess.EntityFramework;
using Cinema.DataAccess.Abstract;
using Cinema.DataAccess.Concrete.EntityFramework.Context;
using Cinema.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.DataAccess.Concrete
{
    public class ShowtimeEntityDal : EfEntityRepositoryBase<ShowtimeEntity, CinemaContext>, IShowtimeEntityDal
    {
      
        public ShowtimeEntityDal(CinemaContext context) : base(context)
        {

        }

    }
}
