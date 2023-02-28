using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;

namespace ApiApplication.Core.Swagger
{
    public class AuthorizationHeaderParameterOperationFilter : IOperationFilter
    {
        private const string READ_ONLY_TOKEN = "MTIzNHxSZWFk";
        private const string WRITE_TOKEN = "MTIzNHxSZWFk";

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            bool hasAuthorization = context.MethodInfo.GetCustomAttributes(typeof(AuthorizeAttribute), true).Any();
            string defaultValue = context.MethodInfo.GetCustomAttributes(typeof(HttpGetAttribute), true).Any()
                                  ? READ_ONLY_TOKEN
                                  : WRITE_TOKEN;

            if (hasAuthorization)
            {
                if (operation.Parameters == null)
                    operation.Parameters = new List<OpenApiParameter>();

                operation.Parameters.Add(new OpenApiParameter
                {
                    Name = "ApiKey",
                    In = ParameterLocation.Header,
                    Description = "access token",
                    Required = true,
                    Schema = new OpenApiSchema
                    {
                        Type = "string",
                        Default = new OpenApiString(defaultValue)
                    }
                });
            }
        }
    }
}
