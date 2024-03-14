using ExevopanNotification.ApplicationCore.Interfaces.Services;
using ExevopanNotification.Domain.Config;
using Microsoft.Extensions.DependencyInjection;

namespace ExevopanNotification.ApplicationCore.CronJob
{
    public class ExevopanNotificationJob : CronJobService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public ExevopanNotificationJob(IScheduleConfig<RuleBreakerNotificationJob> config, IServiceScopeFactory serviceScopeFactory) : base(config.CronExpression, config.TimeZoneInfo)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            using var scope = _serviceScopeFactory.CreateScope();

            var exevoPanService = scope.ServiceProvider.GetService<IExevoPanService>()!;

            await exevoPanService.FindAndNotify();
        }
    }
}
