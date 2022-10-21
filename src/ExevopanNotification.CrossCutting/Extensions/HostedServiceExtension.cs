using ExevopanNotification.ApplicationCore.HostedServices;
using Microsoft.Extensions.DependencyInjection;

namespace ExevopanNotification.CrossCutting.Extensions
{
    public static class HostedServiceExtension
    {
        public static IServiceCollection AddCustomHostedService(this IServiceCollection services)
        {
            services.AddHostedService<ExevopanNotificationHostedService>();

            return services;
        }
    }
}
