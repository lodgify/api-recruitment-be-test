using Microsoft.Extensions.DependencyInjection;
using static ApiApplication.Startup;

namespace ApiApplication
{
    public static class JsonPolicyExtensions
    {
        public static void ApplySnakeCasePolicie(this IMvcBuilder mvcBuilder)
        {
            mvcBuilder.AddJsonOptions(options =>
             {
                 options.JsonSerializerOptions.PropertyNamingPolicy = SnakeCaseNamingPolicy.Instance;
             });
        }
    }

}