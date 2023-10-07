using ExevopanNotification.ApplicationCore.Interfaces.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ExevopanNotification.CrossCutting.Extensions
{
    public static class OnShutdownExtension
    {
        public static IApplicationBuilder UseCustomShutdownHandler(this IApplicationBuilder app)
        {
            var hostApplicationLifetime = app.ApplicationServices.GetRequiredService<IHostApplicationLifetime>();


            hostApplicationLifetime.ApplicationStopping.Register(() =>
            {
                var notifyService = app.ApplicationServices.GetRequiredService<INotifyService>();
                notifyService.NotifyTelegram($"Shuting down - {DateTime.Now}").GetAwaiter();
            }, true);

            return app;
        }
    }
}
