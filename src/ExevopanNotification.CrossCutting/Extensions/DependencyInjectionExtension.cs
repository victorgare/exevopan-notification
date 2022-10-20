using ExevopanNotification.ApplicationCore.Interfaces.Repositories;
using ExevopanNotification.Repository.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExevopanNotification.CrossCutting.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            #region Repositories
            services.AddScoped<IExevoPanRepository, ExevoPanRepository>();
            #endregion

            #region HttpClients
            services.AddHttpClient<ExevoPanRepository>(c => c.BaseAddress = new Uri("https://prod-auctions.service-exevopan.com/"));
            #endregion

            return services;
        }
    }
}
