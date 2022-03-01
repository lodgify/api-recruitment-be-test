using Cinema.Core.DataAccess.EntityFramework;
using Cinema.DataAccess.Abstract;
using Cinema.DataAccess.Concrete.EntityFramework.Context;
using Cinema.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cinema.DataAccess.Concrete
{
    public class MovieEntityDal : EfEntityRepositoryBase<MovieEntity, CinemaContext>, IMovieEntityDal
    {
        public MovieEntityDal(CinemaContext context) : base(context)
        {

        }
    }
}
