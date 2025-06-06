﻿using ExevopanNotification.ApplicationCore.Interfaces.Repositories;
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
            services.AddHttpClient<ExevoPanRepository>(c => c.BaseAddress = new Uri("https://www.exevopan.com/api/"));
            #endregion

            #region Repositories
            services.AddScoped<IExevoPanRepository, ExevoPanRepository>();
            #endregion


            #region Services
            services.AddScoped<IExevoPanService, ExevoPanService>();
            services.AddScoped<IPriceTrendService, PriceTrendService>();
            services.AddScoped<IRuleBreakerService, RuleBreakerService>();
            services.AddScoped<IHardcoreNotifyService, HardcoreNotifyService>();
            #endregion

            #region Notifications
            services.AddSingleton<INotifyService, NotifyService>();
            services.AddSingleton<IAuctionNotification, TelegramService>();
            #endregion

            return services;
        }
    }
}
