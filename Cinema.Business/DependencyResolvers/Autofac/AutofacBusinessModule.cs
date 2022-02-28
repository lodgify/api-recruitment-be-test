using Autofac;
using Cinema.Business.Abstract;
using Cinema.Business.Concrete;
using Cinema.Business.ConfigurationHelper;
using Cinema.DataAccess.Abstract;
using Cinema.DataAccess.Concrete;

namespace Cinema.Business.DependencyResolvers.Autofac
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Showtime
            builder.RegisterType<ShowtimeService>().As<IShowtimeService>();
            builder.RegisterType<ShowtimeEntityDal>().As<IShowtimeEntityDal>();
            builder.RegisterType<AuditoriumEntityDal>().As<IAuditoriumEntityDal>();
            builder.RegisterType<MovieEntityDal>().As<IMovieEntityDal>();
            builder.RegisterType<AppConfiguration>().As<IAppConfiguration>();

            // Status
            builder.RegisterType<StatusService>().As<IStatusService>();    
        }
    }
}
