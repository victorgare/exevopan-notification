using ExevopanNotification.ApplicationCore.Interfaces.Services;
using ExevopanNotification.Domain.Config;
using Microsoft.Extensions.DependencyInjection;

namespace ExevopanNotification.ApplicationCore.CronJob
{
    public class HardcoreNotificationJob : CronJobService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public HardcoreNotificationJob(IScheduleConfig<HardcoreNotificationJob> config, IServiceScopeFactory serviceScopeFactory) : base(config.CronExpression, config.TimeZoneInfo)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var hardcoreNotifyService = scope.ServiceProvider.GetService<IHardcoreNotifyService>()!;

            await hardcoreNotifyService.FindAndNotify();
        }
    }
}
