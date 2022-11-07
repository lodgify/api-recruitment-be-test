using System.Text.Json;
using System.Threading.Tasks;

using ApiApplication.Models;

using Microsoft.AspNetCore.Mvc.Formatters;

namespace ApiApplication.Infra {
    public class ResultFormatter : SystemTextJsonOutputFormatter {
        public ResultFormatter(JsonSerializerOptions jsonSerializerOptions)
            : base(jsonSerializerOptions) {
        }

        public override Task WriteAsync(OutputFormatterWriteContext context) {
            if (context.Object is Result result)
                context.HttpContext.Response.StatusCode = (int)result.Code;

            return base.WriteAsync(context);
        }
    }
}
