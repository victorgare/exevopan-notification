using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExevopanNotification.CrossCutting.Extensions
{
    public static class HealthCheckExtension
    {
        public static IServiceCollection AddCustomHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var healtCheckBuilder = services.AddHealthChecks();
            var sqlConnection = configuration.GetValue<string>("SqlConnectionString");

            healtCheckBuilder.AddUrlGroup(new Uri("https://www.exevopan.com/"), name: "Exevopan");
            healtCheckBuilder.AddUrlGroup(new Uri("https://prod-auctions.service-exevopan.com/"), httpMethod: HttpMethod.Post, name: "Exevopan-backend");
            return services;
        }

        public static IApplicationBuilder UseCustomHealthCheck(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/hc", new HealthCheckOptions
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
                AllowCachingResponses = false
            });

            return app;
        }
    }
}
