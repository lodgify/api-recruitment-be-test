using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Options;

namespace ApiApplication.Infra {
    public class ResultFormatterSetup : IConfigureOptions<MvcOptions> {
        private readonly JsonOptions _jsonOption;

        public ResultFormatterSetup(IOptions<JsonOptions> jsonOption) {
            _jsonOption = jsonOption.Value;
        }

        public void Configure(MvcOptions options) {
            IOutputFormatter oldFormatter = options.OutputFormatters.FirstOrDefault(f => f.GetType() == typeof(SystemTextJsonOutputFormatter));
            if (oldFormatter != null)
                options.OutputFormatters.Remove(oldFormatter);
            options.OutputFormatters.Add(new ResultFormatter(_jsonOption.JsonSerializerOptions));
        }
    }
}
