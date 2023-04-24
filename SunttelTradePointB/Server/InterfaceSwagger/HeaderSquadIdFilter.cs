using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SunttelTradePointB.Server.InterfaceSwagger
{
    /// <summary>
    /// Operation filter to add the requirement of the custom header
    /// </summary>
    public class HeaderSquadIdFilter : Attribute, IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Parameters.Add(new OpenApiParameter
            {
                Name = "SquadId",
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema { Type = "string" },
                Required = true
                
            }); ;
        }
    }
}