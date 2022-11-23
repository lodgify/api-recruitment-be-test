using ApiApplication.Helpers;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace ApiApplication.Extensions
{
    public static class AutoMapperExtension
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            services.AddSingleton(mapperConfig.CreateMapper());
        }
    }
}
