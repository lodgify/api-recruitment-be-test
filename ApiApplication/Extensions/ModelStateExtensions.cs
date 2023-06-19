using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApplication.Extensions
{
    public static class ModelStateExtensions
    {
        public static IEnumerable<KeyValuePair<string, string[]>> Errors(this ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                return modelState.ToDictionary(kvp => kvp.Key,
                    kvp => kvp.Value.Errors
                                    .Select(e => e.ErrorMessage).ToArray())
                                    .Where(m => m.Value.Count() > 0);
            }

            return null;
        }

        public static string ErrorsMessage(this ModelStateDictionary modelState)
        {
            string result = string.Empty;

            if (!modelState.IsValid)
            {
                foreach (var key in (IEnumerable<KeyValuePair<string, string[]>>)modelState.ToDictionary(kvp => kvp.Key,
                    kvp => kvp.Value.Errors
                                    .Select(e => e.ErrorMessage).ToArray())
                                    .Where(m => m.Value.Count() > 0))
                {
                    result += string.Join("\n", key.Value);
                }
            }

            return result;
        }

        public static string[] ErrorMessages(this ModelStateDictionary modelState)
        {
            List<string> result = new List<string>();

            if (!modelState.IsValid)
            {
                foreach (var key in (IEnumerable<KeyValuePair<string, string[]>>)modelState.ToDictionary(kvp => kvp.Key,
                    kvp => kvp.Value.Errors
                                    .Select(e => e.ErrorMessage).ToArray())
                                    .Where(m => m.Value.Count() > 0))
                {
                    result.AddRange(key.Value);
                }
            }

            return result.ToArray();
        }
    }
}
