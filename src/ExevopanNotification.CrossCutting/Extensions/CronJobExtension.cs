using ExevopanNotification.ApplicationCore.CronJob;
using ExevopanNotification.Domain.Config;
using Microsoft.Extensions.DependencyInjection;

namespace ExevopanNotification.CrossCutting.Extensions
{
    public static class CronJobExtension
    {
        public static IServiceCollection AddCustomCronJob(this IServiceCollection services)
        {
            services.AddCronJob<RuleBreakerNotificationCronJob>(c =>
            {
                c.TimeZoneInfo = TimeZoneInfo.Utc;

                // every day at 10h
                c.CronExpression = "0 10 * * *";
            });

            return services;
        }
        public static IServiceCollection AddCronJob<T>(this IServiceCollection services, Action<IScheduleConfig<T>> options) where T : CronJobService
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options), @"Please provide Schedule Configurations.");
            }
            var config = new ScheduleConfig<T>();
            options.Invoke(config);
            if (string.IsNullOrWhiteSpace(config.CronExpression))
            {
                throw new ArgumentNullException(nameof(options), "Empty Cron Expression is not allowed.");
            }

            services.AddSingleton<IScheduleConfig<T>>(config);
            services.AddHostedService<T>();
            return services;
        }
    }
}
