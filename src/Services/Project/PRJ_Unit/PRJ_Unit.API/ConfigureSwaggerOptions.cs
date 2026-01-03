using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace PRJ_Unit.API
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
         
        public ConfigureSwaggerOptions(
            IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        /// <summary>
        /// Configure each API discovered for Swagger Documentation
        /// </summary>
        /// <param name="options"></param> 
        public void Configure(SwaggerGenOptions option)
        {
            // add swagger document for every API version discovered
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                option.SwaggerDoc(description.GroupName, new OpenApiInfo { Title = "PRJ-Unit-Service", Version = description.GroupName });
            }
        }

        /// <summary>
        /// Configure Swagger Options. Inherited from the Interface
        /// </summary>
        /// <param name="name"></param>
        /// <param name="options"></param>
        public void Configure(string? name, SwaggerGenOptions option)
        {
            Configure(option);
        }
    }
}
