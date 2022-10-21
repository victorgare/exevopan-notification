using ExevopanNotification.ApplicationCore.Interfaces.Repositories;
using ExevopanNotification.ApplicationCore.Interfaces.Services;
using ExevopanNotification.ApplicationCore.Services;
using ExevopanNotification.Repository.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExevopanNotification.CrossCutting.Extensions
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            #region HttpClients
            services.AddHttpClient<ExevoPanRepository>(c => c.BaseAddress = new Uri("https://prod-auctions.service-exevopan.com"));
            #endregion

            #region Repositories
            services.AddScoped<IExevoPanRepository, ExevoPanRepository>();
            #endregion


            #region Services
            services.AddScoped<IExevoPanService, ExevoPanService>();
            #endregion

            #region Notifications
            services.AddScoped<INotifyService, NotifyService>();
            services.AddScoped<IAuctionNotification, TelegramService>();
            #endregion

            return services;
        }
    }
}
