using Microsoft.Extensions.DependencyInjection;

namespace ApiApplication.Extensions
{
    public static class ValidationExtension
    {
        public static void AddFluentValidation(this IServiceCollection services)
        {
            // services.AddValidatorsFromAssemblyContaining<AddUserModelValidation>();
        }
    }
}
