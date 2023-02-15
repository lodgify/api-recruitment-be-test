using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Linq;

namespace ApiApplication.Extensions
{
    public static class ModelStateExtension
    {
        public static Dictionary<string, List<string>> GetErrorMessages(this ModelStateDictionary modelState)
        {
            return modelState.Where(x => x.Value.Errors.Count > 0)
                             .ToDictionary(x => x.Key,
                                           x => x.Value.Errors.Select(e => e.ErrorMessage)
                             .ToList());
        }
    }
}
