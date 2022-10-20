using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace ExevopanNotification.CrossCutting.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();

            services.AddApiVersioning(options =>
            {
                // Retorna os headers "api-supported-versions" e "api-deprecated-versions"
                // indicando versões suportadas pela API e o que está como deprecated
                options.ReportApiVersions = true;

                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
            });

            services.AddVersionedApiExplorer(options =>
            {
                // Agrupar por número de versão
                options.GroupNameFormat = "'v'VVV";

                // Necessário para o correto funcionamento das rotas
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(options =>
            {
                var provider = services.BuildServiceProvider();
                var service = provider.GetRequiredService<IApiVersionDescriptionProvider>();

                service.ApiVersionDescriptions.ToList().ForEach(apiVersionDescription =>
                {
                    var apiVersion = apiVersionDescription.ApiVersion.ToString();
                    var descriptipn = $"APIs para o ExevoPan Notification - V{apiVersion}";

                    if (apiVersionDescription.IsDeprecated)
                    {
                        descriptipn.Concat(" - (Deprecated)");
                    }

                    options.SwaggerDoc(apiVersionDescription.GroupName, new OpenApiInfo
                    {
                        Title = "ExevoPan Notification",
                        Version = apiVersion,
                        Description = descriptipn,
                    });
                });

            });

            return services;
        }


        public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

                provider.ApiVersionDescriptions.ToList().ForEach(description =>
                {
                    c.RoutePrefix = "swagger";
                    c.SwaggerEndpoint($"{description.GroupName}/swagger.json", $"ExevoPan Notification - {description.GroupName}");
                });
            });

            return app;
        }
    }
}
